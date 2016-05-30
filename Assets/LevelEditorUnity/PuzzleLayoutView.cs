using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.LevelEditorUnity
{
    // TODO: REMOVE
    [Serializable]
    public class SerializableTilePos
    {
        public int X;
        public int Z;

        public SerializableTilePos(TilePos position)
        {
            X = position.X;
            Z = position.Z;
        }

        public TilePos AsTilePosition()
        {
            return new TilePos(X, Z);
        }
    }

    // TODO: REMOVE
    [Serializable]
    public class SerializableNode
    {
        public SerializableTilePos Position;
        public List<SerializableTilePos> Connected;

        public SerializableNode(Node node)
        {
            Position = new SerializableTilePos(node.Position);
            Connected = node.Connections.Select(x => new SerializableTilePos(x.Position)).ToList();
        }

        public Node AsNode()
        {
            return new Node(Position.AsTilePosition());
        }
    }

    // TODO: REMOVE
    [Serializable]
    public class SerializableNodes
    {
        public List<SerializableNode> Nodes;

        public SerializableNodes(List<SerializableNode> nodes)
        {
            Nodes = nodes;
        }
    }

    [ExecuteInEditMode]
    public class PuzzleLayoutView : MonoBehaviour
    {
        public GameObject Connector;
        public GameObject Node;

        public Dictionary<TilePos, GameObject> NodeViews = new Dictionary<TilePos, GameObject>();
        public Dictionary<NodeConnection, GameObject> NodeConnectionViews = new Dictionary<NodeConnection, GameObject>();

        // TODO: REMOVE
        [SerializeField]
        private string _nodeData;

        // TODO: REMOVE
        public void SaveAsSerializable()
        {
            var nodes = PuzzleLayout.Instance.Nodes.Values;
            var serializableNodes = new SerializableNodes(nodes.Select(node => new SerializableNode(node)).ToList());
            _nodeData = JsonUtility.ToJson(serializableNodes);
            //_serializedNodes = nodes.Select(node => new SerializableNode(node)).ToArray();
        }

        // TODO: REMOVE
        public void LoadFromSerializable()
        {
            var serializedNodes = JsonUtility.FromJson<SerializableNodes>(_nodeData);
            if (serializedNodes == null)
            {
                return;
            }

            PuzzleLayout.Instance.Clear();
            var nodes = PuzzleLayout.Instance.Nodes;

            foreach (var serializedNode in serializedNodes.Nodes)
            {
                nodes.Add(serializedNode.Position.AsTilePosition(), serializedNode.AsNode());
            }
            foreach (var serializedNode in serializedNodes.Nodes)
            {
                var node = nodes[serializedNode.Position.AsTilePosition()];
                node.Connections.AddRange(serializedNode.Connected.Select(x => nodes[x.AsTilePosition()]).ToList());
            }
        }

        private void LoadLevelStateFromScene()
        {
            var connectors = gameObject.GetChildren("Connector", true).Select(x => x.transform);

            foreach (var connector in connectors)
            {
                var start = connector.localPosition;
                var end = start + connector.rotation*Vector3.forward*TilePos.TileLength;
                var nodeConnection = new NodeConnection(new TilePos(start), new TilePos(end));
                PuzzleLayout.Instance.AddNodeConnection(nodeConnection);
            }
        }

        public void OnEnable()
        {
            LoadLevelStateFromScene();
            PuzzleLayout.Instance.NodeAdded += AddNode;
            PuzzleLayout.Instance.NodeRemoved += RemoveNode;
            PuzzleLayout.Instance.ConnectionAdded += AddNodeConnection;
            PuzzleLayout.Instance.ConnectionRemoved += RemoveConnection;
        }

        public void OnDisable()
        {
            PuzzleLayout.Instance.NodeAdded -= AddNode;
            PuzzleLayout.Instance.NodeRemoved -= RemoveNode;
            PuzzleLayout.Instance.ConnectionAdded -= AddNodeConnection;
            PuzzleLayout.Instance.ConnectionRemoved -= RemoveConnection;
        }

        private void AddNode(Node node)
        {
            if (!NodeViews.ContainsKey(node.Position))
            {
                var nodeView = (GameObject)Instantiate(
                    Node,
                    node.Position.ToV3(),
                    Quaternion.identity);
                nodeView.transform.SetParent(transform);
                NodeViews.Add(node.Position, nodeView);
            }
        }

        private void RemoveNode(Node node)
        {
            if (NodeViews.ContainsKey(node.Position))
            {
                DestroyImmediate(NodeViews[node.Position]);
                NodeViews.Remove(node.Position);
            }
        }

        public void AddNodeConnection(NodeConnection connection)
        {
            if (!NodeConnectionViews.ContainsKey(connection))
            {
                var connectionView = (GameObject)Instantiate(
                    Connector,
                    connection.Start.ToV3(),
                    Quaternion.FromToRotation(Vector3.forward, (connection.End - connection.Start).ToV3()));
                connectionView.transform.SetParent(transform);
                NodeConnectionViews.Add(connection, connectionView);
            }
        }

        public void RemoveConnection(NodeConnection connection)
        {
            if (NodeConnectionViews.ContainsKey(connection))
            {
                DestroyImmediate(NodeConnectionViews[connection]);
                NodeConnectionViews.Remove(connection);
            }
        }
    }
}