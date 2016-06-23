using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.LevelEditorUnity
{
    public class PuzzleLayout
    {
        private static PuzzleLayout _instance;
        public static PuzzleLayout Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PuzzleLayout();
                }
                return _instance;
            }
        }

        private class PuzzleObjectCollection
        {
            public List<TilePos> Objects { get; private set; }

            public PuzzleObjectCollection()
            {
                Objects = new List<TilePos>();
            }

            public PuzzleObjectCollection Concat(TilePos position)
            {
                Objects.Add(position);
                return this;
            }
        }

        private readonly Dictionary<string, PuzzleObjectCollection> _puzzleObjects = new Dictionary<string, PuzzleObjectCollection>();

        public void SetSingleton(string type, TilePos? position)
        {
            if (_puzzleObjects.ContainsKey(type))
            {
                var worldObject = _puzzleObjects[type];
                _puzzleObjects.Remove(type);
                ObjectRemoved.CallEvent(type, worldObject.Objects.First());
            }

            if (position.HasValue)
            {
                PlaceObject(type, position.Value);
            }
        }

        public void PlaceObject(string type, TilePos position)
        {
            RemoveObject(position);

            var collection = _puzzleObjects.ContainsKey(type)
                ? _puzzleObjects[type]
                : new PuzzleObjectCollection();

            _puzzleObjects[type] = collection.Concat(position);
            ObjectAdded.CallEvent(type, position);
        }

        public void RemoveObject(TilePos position)
        {
            var currentAtSamePosition = GetObjectAt(position);

            if (currentAtSamePosition != null)
            {
                var objects = _puzzleObjects[currentAtSamePosition].Objects;
                if (objects.Count <= 1)
                {
                    _puzzleObjects.Remove(currentAtSamePosition);
                }
                else
                {
                    objects.Remove(position);
                }

                ObjectRemoved.CallEvent(currentAtSamePosition, position);
            }
        }

        public string GetObjectAt(TilePos position)
        {
            var otherAtSamePosition = _puzzleObjects.SingleOrDefault(x => x.Value.Objects.Any(pos => pos == position));
            return otherAtSamePosition.Key;
        }

        public TilePos? GetSingleton(string type)
        {
            return _puzzleObjects.ContainsKey(type) ? _puzzleObjects[type].Objects.First() : (TilePos?) null;
        }

        public List<TilePos> GetObjects(string type)
        {
            return _puzzleObjects.ContainsKey(type) ? _puzzleObjects[type].Objects : new List<TilePos>();
        }

        public bool CanPlaceAt(TilePos position)
        {
            return Nodes.ContainsKey(position);
        }

        public event Action<string, TilePos> ObjectAdded;
        public event Action<string, TilePos> ObjectRemoved;

        public readonly Dictionary<TilePos, Node> Nodes = new Dictionary<TilePos, Node>();

        public event Action<Node> NodeAdded; 
        public event Action<Node> NodeRemoved;
        public event Action<NodeConnection> ConnectionAdded;
        public event Action<NodeConnection> ConnectionRemoved;

        public void AddNodeConnections(NodeConnection wholeConnection)
        {
            wholeConnection
                .GetSubdividedConnection()
                .ToList()
                .ForEach(x => AddSubdividedConnection(x));
        }

        private bool AddSubdividedConnection(NodeConnection connection)
        {
            var node1 = GetExistingNodeOrCreateNew(connection.Start);
            var node2 = GetExistingNodeOrCreateNew(connection.End);

            var alreadyHasConnection = node1.Connections.Contains(node2) || node2.Connections.Contains(node1);
            if (alreadyHasConnection)
            {
                return false;
            }

            node1.Connections.Add(node2);
            node2.Connections.Add(node1);

            ConnectionAdded.CallEvent(connection);
            return true;
        }

        public void RemoveNodeConnection(NodeConnection wholeConnection)
        {
            wholeConnection
                .GetSubdividedConnection()
                .ToList()
                .ForEach(RemoveSubdividedConnection);
        }

        private void RemoveSubdividedConnection(NodeConnection connection)
        {
            var eitherNodeDoesntExistInLayout = !Nodes.ContainsKey(connection.Start) || !Nodes.ContainsKey(connection.End);
            if (eitherNodeDoesntExistInLayout)
            {
                return;
            }

            var node1 = Nodes[connection.Start];
            var node2 = Nodes[connection.End];

            if (!AreConnected(node1, node2))
            {
                return;
            }

            RemoveOneWayConnection(node1, node2);
            RemoveOneWayConnection(node2, node1);

            ConnectionRemoved.CallEvent(connection);
        }

        private bool AreConnected(Node node1, Node node2)
        {
            return node1.Connections.Contains(node2) && node2.Connections.Contains(node1);
        }

        private Node GetExistingNodeOrCreateNew(TilePos position)
        {
            if (Nodes.ContainsKey(position))
            {
                return Nodes[position];
            }

            var newNode = new Node(position);
            Nodes.Add(position, newNode);
            NodeAdded.CallEvent(newNode);
            return newNode;
        }

        private void RemoveOneWayConnection(Node node1, Node node2)
        {
            node1.Connections.Remove(node2);

            if (node1.Connections.Count == 0)
            {
                var singletonAtSamePosition = GetObjectAt(node1.Position);
                if (singletonAtSamePosition != null)
                {
                    RemoveObject(node1.Position);
                }

                Nodes.Remove(node1.Position);
                NodeRemoved.CallEvent(node1);
            }
        }

        public void Clear()
        {
            while (Nodes.Count > 0)
            {
                var node = Nodes.Values.First();
                while (node.Connections.Count > 0)
                {
                    var connection = node.Connections[0];
                    RemoveNodeConnection(new NodeConnection(node.Position, connection.Position));
                }
            }
        }

        public List<NodeConnection> GetAllConnections()
        {
            var nodeConnections = new HashSet<NodeConnection>();

            foreach (var node in Nodes.Values)
            {
                foreach (var otherNode in node.Connections)
                {
                    var connection = new NodeConnection(node.Position, otherNode.Position);
                    if (!nodeConnections.Contains(connection))
                    {
                        nodeConnections.Add(connection);
                    }
                }
            }

            return nodeConnections.ToList();
        }
    }
}