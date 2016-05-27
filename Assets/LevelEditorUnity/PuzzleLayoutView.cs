using System.Collections.Generic;
using UnityEngine;

namespace Assets.LevelEditorUnity
{
    [ExecuteInEditMode]
    public class PuzzleLayoutView : MonoBehaviour
    {
        public GameObject Connector;
        public GameObject Node;

        public Dictionary<TilePos, GameObject> NodeViews = new Dictionary<TilePos, GameObject>();
        public Dictionary<NodeConnection, GameObject> NodeConnectionViews = new Dictionary<NodeConnection, GameObject>();

        public void OnEnable()
        {
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