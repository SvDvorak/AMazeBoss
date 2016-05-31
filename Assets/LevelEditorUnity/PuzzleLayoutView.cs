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

        private GameObject _preview;

        private PuzzleLayout PuzzleLayout { get { return PuzzleLayout.Instance; } }

        private void LoadLevelStateFromScene()
        {
            PuzzleLayout.Instance.Clear();
            var nodes = gameObject.GetChildren("Node", true);

            foreach (var node in nodes)
            {
                NodeViews.Add(new TilePos(node.transform.localPosition), node);
            }

            var connectors = gameObject.GetChildren("Connector", true);

            foreach (var connector in connectors)
            {
                var start = connector.transform.localPosition;
                var end = start + connector.transform.rotation*Vector3.forward*TilePos.TileLength;
                var nodeConnection = new NodeConnection(new TilePos(start), new TilePos(end));
                PuzzleLayout.Instance.AddNodeConnections(nodeConnection);
                NodeConnectionViews.Add(nodeConnection, connector);
            }
        }

        public void OnEnable()
        {
            LoadLevelStateFromScene();
            PuzzleLayout.NodeAdded += AddNode;
            PuzzleLayout.NodeRemoved += RemoveNode;
            PuzzleLayout.ConnectionAdded += AddNodeConnection;
            PuzzleLayout.ConnectionRemoved += RemoveConnection;
        }

        public void OnDisable()
        {
            PuzzleLayout.NodeAdded -= AddNode;
            PuzzleLayout.NodeRemoved -= RemoveNode;
            PuzzleLayout.ConnectionAdded -= AddNodeConnection;
            PuzzleLayout.ConnectionRemoved -= RemoveConnection;
        }

        private void AddNode(Node node)
        {
            if (!NodeViews.ContainsKey(node.Position))
            {
                var nodeView = CreateNodeView(node);
                NodeViews.Add(node.Position, nodeView);
            }
        }

        private GameObject CreateNodeView(Node node)
        {
            var nodeView = (GameObject) Instantiate(
                Node,
                node.Position.ToV3(),
                Quaternion.identity);
            nodeView.transform.SetParent(transform);
            return nodeView;
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
                var connectionView = CreateNodeConnectionView(connection);
                NodeConnectionViews.Add(connection, connectionView);
            }
        }

        private GameObject CreateNodeConnectionView(NodeConnection connection)
        {
            var connectionView = (GameObject) Instantiate(
                Connector,
                connection.Start.ToV3(),
                GetRotationFromConnectionEnd(connection));
            connectionView.transform.SetParent(transform);
            return connectionView;
        }

        private static Quaternion GetRotationFromConnectionEnd(NodeConnection connection)
        {
            return Quaternion.FromToRotation(Vector3.forward, (connection.End - connection.Start).ToV3());
        }

        public void RemoveConnection(NodeConnection connection)
        {
            if (NodeConnectionViews.ContainsKey(connection))
            {
                DestroyImmediate(NodeConnectionViews[connection]);
                NodeConnectionViews.Remove(connection);
            }
        }

        public void UpdatePreview(NodeConnection nodeConnection)
        {
            if (_preview == null)
            {
                _preview = CreateNodeConnectionView(nodeConnection);
                _preview.name = "Preview";
            }
            else
            {
                _preview.transform.localPosition = nodeConnection.Start.ToV3();
                _preview.transform.rotation = GetRotationFromConnectionEnd(nodeConnection);
            }
        }

        public void RemovePreview()
        {
            if (_preview != null)
            {
                DestroyImmediate(_preview);
                _preview = null;
            }
        }
    }
}