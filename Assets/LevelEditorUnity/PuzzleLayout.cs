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

        private readonly Dictionary<string, TilePos?> _singletons = new Dictionary<string, TilePos?>();

        public void SetSingleton(string type, TilePos? position)
        {
            var currentAtSamePosition = GetSingletonAt(position);

            if (currentAtSamePosition != null)
            {
                SetSingleton(currentAtSamePosition, null);
            }

            if (_singletons.ContainsKey(type))
            {
                var removedPosition = _singletons[type];
                _singletons.Remove(type);
                SingletonRemoved.CallEvent(type, removedPosition);
            }

            if (position.HasValue)
            {
                _singletons[type] = position;
                SingletonAdded.CallEvent(type, position.Value);
            }
        }

        private string GetSingletonAt(TilePos? position)
        {
            if (position != null)
            {
                var otherAtSamePosition = _singletons.SingleOrDefault(x => x.Value == position);

                if (otherAtSamePosition.Key != null)
                {
                    return otherAtSamePosition.Key;
                }
            }

            return null;
        }

        public TilePos? GetSingleton(string type)
        {
            return _singletons.ContainsKey(type) ? _singletons[type] : null;
        }

        public event Action<string, TilePos> SingletonAdded;
        public event Action<string, TilePos?> SingletonRemoved;

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
                var singletonAtSamePosition = GetSingletonAt(node1.Position);
                if (singletonAtSamePosition != null)
                {
                    SetSingleton(singletonAtSamePosition, null);
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