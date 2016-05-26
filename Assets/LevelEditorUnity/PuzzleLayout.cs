using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEditor;
using UnityEngine;

public class PuzzleLayout : MonoBehaviour
{
    public GameObject Connector;
    public GameObject Node;

    public Dictionary<TilePos, Node> Nodes = new Dictionary<TilePos, Node>();

    public Dictionary<TilePos, GameObject> NodeViews = new Dictionary<TilePos, GameObject>();
    public Dictionary<NodeConnection, GameObject> NodeConnectionViews = new Dictionary<NodeConnection, GameObject>();

    public void AddNodeConnection(NodeConnection connection)
    {
        AddNodeIfNoneExistsAt(connection.Start);
        AddNodeIfNoneExistsAt(connection.End);

        var connectionView = (GameObject)Instantiate(
            Connector,
            connection.Start.ToV3(),
            Quaternion.FromToRotation(Vector3.forward, (connection.End - connection.Start).ToV3()));
        Undo.RegisterCreatedObjectUndo(connectionView, "Created connection");

        NodeConnectionViews.Add(connection, connectionView);

        var startNode = Nodes[connection.Start];
        var endNode = Nodes[connection.End];

        startNode.Connections.Add(endNode);
        endNode.Connections.Add(startNode);
    }

    public void RemoveConnection(NodeConnection connection)
    {
        if (!Nodes.ContainsKey(connection.Start) || !Nodes.ContainsKey(connection.End))
        {
            return;
        }

        var startNode = Nodes[connection.Start];
        var endNode = Nodes[connection.End];
        startNode.Connections.Remove(endNode);
        endNode.Connections.Remove(startNode);

        if (!startNode.Connections.Any())
        {
            Destroy(NodeViews[connection.Start]);
            Nodes.Remove(connection.Start);
        }
        if (!endNode.Connections.Any())
        {
            Destroy(NodeViews[connection.End]);
            Nodes.Remove(connection.End);
        }
    }

    private void AddNodeIfNoneExistsAt(TilePos position)
    {
        if (!Nodes.ContainsKey(position))
        {
            var nodeView = Instantiate(
                Node,
                position.ToV3(),
                Quaternion.identity);
            Undo.RegisterCreatedObjectUndo(nodeView, "Created node");

            Nodes.Add(position, new Node());
        }
    }
}

public struct NodeConnection
{
    public TilePos Start { get; set; }
    public TilePos End { get; set; }

    public bool Equals(NodeConnection other)
    {
        return Start.Equals(other.Start) && End.Equals(other.End);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is NodeConnection && Equals((NodeConnection) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Start.GetHashCode()*397) ^ End.GetHashCode();
        }
    }
}

public class Node
{
    public List<Node> Connections = new List<Node>();
}