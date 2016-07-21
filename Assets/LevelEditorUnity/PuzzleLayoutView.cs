using System.Collections.Generic;
using UnityEngine;

namespace Assets.LevelEditorUnity
{
    [ExecuteInEditMode]
    public class PuzzleLayoutView : MonoBehaviour
    {
        public GameObject Connector;
        public GameObject Node;
        public GameObject Player;
        public GameObject Boss;
        public GameObject Trap;
        public GameObject TrapItem;

        public Dictionary<TilePos, GameObject> NodeViews = new Dictionary<TilePos, GameObject>();
        public Dictionary<NodeConnection, GameObject> NodeConnectionViews = new Dictionary<NodeConnection, GameObject>();

        private List<GameObject> _previews = new List<GameObject>();
        private readonly Dictionary<TilePos, GameObject> _worldObjects = new Dictionary<TilePos, GameObject>();
        private NodeConnection _lastPreviewConnection;

        private PuzzleLayout PuzzleLayout { get { return PuzzleLayout.Instance; } }

        public void OnEnable()
        {
            PuzzleLayout.NodeAdded += AddNode;
            PuzzleLayout.NodeRemoved += RemoveNode;
            PuzzleLayout.ConnectionAdded += AddNodeConnection;
            PuzzleLayout.ConnectionRemoved += RemoveConnection;
            PuzzleLayout.ObjectAdded += ObjectAdded;
            PuzzleLayout.ObjectRemoved += ObjectRemoved;

            if (Application.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }

        public void OnDisable()
        {
            RemovePreview();
            PuzzleLayout.NodeAdded -= AddNode;
            PuzzleLayout.NodeRemoved -= RemoveNode;
            PuzzleLayout.ConnectionAdded -= AddNodeConnection;
            PuzzleLayout.ConnectionRemoved -= RemoveConnection;
            PuzzleLayout.ObjectAdded -= ObjectAdded;
            PuzzleLayout.ObjectRemoved -= ObjectRemoved;
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
            var nodeView = CreateTemporary(Node, node.Position.ToV3(), Quaternion.identity);
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
            var connectionView = CreateTemporary(
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

        private void ObjectAdded(string type, TilePos position)
        {
            var addedObjectView = EditorWorldObjects.Instance.GetByType(type);
            AddWorldObject(addedObjectView, position);
        }

        private void AddWorldObject(GameObject template, TilePos position)
        {
            _worldObjects[position] = CreateTemporary(
                template,
                position.ToV3(),
                Quaternion.identity);

            _worldObjects[position].transform.SetParent(transform);
        }

        private void ObjectRemoved(string type, TilePos position)
        {
            if(_worldObjects.ContainsKey(position))
            {
                DestroyImmediate(_worldObjects[position]);
            }
        }

        public void UpdatePreview(NodeConnection nodeConnection)
        {
            if (!nodeConnection.Equals(_lastPreviewConnection))
            {
                RemovePreview();

                var subConnections = nodeConnection.GetSubdividedConnection();
                foreach (var subConnection in subConnections)
                {
                    AddPreview(CreateNodeConnectionView(subConnection));
                }
                Debug.Log("Recreated preview");
            }

            _lastPreviewConnection = nodeConnection;
        }

        public void UpdatePreview(EditorWorldObject selectedWorldObject, TilePos position)
        {
            RemovePreview();
            AddPreview(CreateTemporary(selectedWorldObject.GameObject, position.ToV3(), Quaternion.identity));
        }

        private void AddPreview(GameObject preview)
        {
            preview.name = "Preview";
            _previews.Add(preview);
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

        private GameObject CreateTemporary(GameObject template, Vector3 position, Quaternion rotation)
        {
            var newObject = (GameObject) Instantiate(
                template,
                position,
                rotation);
            
            newObject.hideFlags = HideFlags.DontSave;

            return newObject;
        }
    }
}