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

        private List<GameObject> _previews = new List<GameObject>();
        private NodeConnection _lastPreviewConnection;

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
            if (!nodeConnection.Equals(_lastPreviewConnection))
            {
                RemovePreview();

                var subConnections = SubdivideConnection(nodeConnection);
                foreach (var subConnection in subConnections)
                {
                    var preview = CreateNodeConnectionView(subConnection);
                    preview.name = "Preview";
                    preview.transform.localPosition = subConnection.Start.ToV3();
                    preview.transform.rotation = GetRotationFromConnectionEnd(subConnection);
                    _previews.Add(preview);
                }
                Debug.Log("Recreated preview");
            }

            _lastPreviewConnection = nodeConnection;
        }

        public void RemovePreview()
        {
            if (_previews != null)
            {
                foreach (var preview in _previews)
                {
                    DestroyImmediate(preview);
                }
                _previews = new List<GameObject>();
            }
        }

        private List<NodeConnection> SubdivideConnection(NodeConnection connection)
        {
            var direction = connection.End - connection.Start;
            var subdivideCount = direction.Length();
            var directionNormalized = direction.Normalized();
            var affectedConnections = new List<NodeConnection>();

            for (int i = 0; i < subdivideCount; i++)
            {
                var subConnectionStart = connection.Start + directionNormalized * i;
                var subConnectionEnd = connection.Start + directionNormalized * (i + 1);
                var subConnection = new NodeConnection(subConnectionStart, subConnectionEnd);

                affectedConnections.Add(subConnection);
            }

            return affectedConnections;
        }
    }
}