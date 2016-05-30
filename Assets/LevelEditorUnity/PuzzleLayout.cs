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

        public readonly Dictionary<TilePos, Node> Nodes = new Dictionary<TilePos, Node>();

        public event Action<Node> NodeAdded; 
        public event Action<Node> NodeRemoved;
        public event Action<NodeConnection> ConnectionAdded;
        public event Action<NodeConnection> ConnectionRemoved;

        public void AddNodeConnections(NodeConnection wholeConnection)
        {
            SubdivideConnectionAndDoForEachSegment(wholeConnection, AddSubdividedConnection);
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

            CallEvent(ConnectionAdded, connection);
            return true;
        }

        public List<NodeConnection> RemoveAndReturnNodeConnections(NodeConnection wholeConnection)
        {
            return SubdivideConnectionAndDoForEachSegment(wholeConnection, RemoveSubdividedConnection);
        }

        private bool RemoveSubdividedConnection(NodeConnection connection)
        {
            var eitherNodeDoesntExistInLayout = !Nodes.ContainsKey(connection.Start) || !Nodes.ContainsKey(connection.End);
            if (eitherNodeDoesntExistInLayout)
            {
                return false;
            }

            var node1 = Nodes[connection.Start];
            var node2 = Nodes[connection.End];

            if (!AreConnected(node1, node2))
            {
                return false;
            }

            RemoveOneWayConnection(node1, node2);
            RemoveOneWayConnection(node2, node1);

            CallEvent(ConnectionRemoved, connection);
            return true;
        }

        private bool AreConnected(Node node1, Node node2)
        {
            return node1.Connections.Contains(node2) && node2.Connections.Contains(node1);
        }

        private List<NodeConnection> SubdivideConnectionAndDoForEachSegment(NodeConnection connection, Func<NodeConnection, bool> action)
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

                var success = action(subConnection);
                if (success)
                {
                    affectedConnections.Add(subConnection);
                }
            }

            return affectedConnections;
        }

        private Node GetExistingNodeOrCreateNew(TilePos position)
        {
            if (Nodes.ContainsKey(position))
            {
                return Nodes[position];
            }

            var newNode = new Node(position);
            Nodes.Add(position, newNode);
            CallEvent(NodeAdded, newNode);
            return newNode;
        }

        private void RemoveOneWayConnection(Node node1, Node node2)
        {
            node1.Connections.Remove(node2);

            if (node1.Connections.Count == 0)
            {
                Nodes.Remove(node1.Position);
                CallEvent(NodeRemoved, node1);
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
                    RemoveAndReturnNodeConnections(new NodeConnection(node.Position, connection.Position));
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

        private void CallEvent<T>(Action<T> callback, T value)
        {
            if (callback != null)
            {
                callback(value);
            }
        }
    }
}