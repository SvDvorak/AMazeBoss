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

        public Dictionary<TilePos, Node> Nodes = new Dictionary<TilePos, Node>();

        public event Action<Node> NodeAdded; 
        public event Action<Node> NodeRemoved;
        public event Action<NodeConnection> ConnectionAdded;
        public event Action<NodeConnection> ConnectionRemoved;

        public void AddNodeConnection(NodeConnection connection)
        {
            SubdivideConnectionAndDoForEachSegment(connection, AddSubdividedConnection);
        }

        private void AddSubdividedConnection(NodeConnection connection)
        {
            var node1 = GetExistingNodeOrCreateNew(connection.Start);
            var node2 = GetExistingNodeOrCreateNew(connection.End);

            var alreadyHasConnection = node1.Connections.Contains(node2) || node2.Connections.Contains(node1);
            if(!alreadyHasConnection)
            {
                node1.Connections.Add(node2);
                node2.Connections.Add(node1);

                OnConnectionAdded(connection);
            }
        }

        public void RemoveNodeConnection(NodeConnection connection)
        {
            SubdivideConnectionAndDoForEachSegment(connection, RemoveSubdividedConnection);
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
            RemoveOneWayConnection(node1, node2);
            RemoveOneWayConnection(node2, node1);

            OnConnectionRemoved(connection);
        }

        private void SubdivideConnectionAndDoForEachSegment(NodeConnection connection, Action<NodeConnection> action)
        {
            var direction = connection.End - connection.Start;
            var subdivideCount = direction.Length();
            var directionNormalized = direction.Normalized();

            for (int i = 0; i < subdivideCount; i++)
            {
                var subConnectionStart = connection.Start + directionNormalized * i;
                var subConnectionEnd = connection.Start + directionNormalized * (i + 1);
                var subConnection = new NodeConnection(subConnectionStart, subConnectionEnd);
                action(subConnection);
            }
        }

        private Node GetExistingNodeOrCreateNew(TilePos position)
        {
            if (Nodes.ContainsKey(position))
            {
                return Nodes[position];
            }

            var newNode = new Node(position);
            Nodes.Add(position, newNode);
            OnNodeAdded(newNode);
            return newNode;
        }

        private void RemoveOneWayConnection(Node node1, Node node2)
        {
            node1.Connections.Remove(node2);

            if (node1.Connections.Count == 0)
            {
                Nodes.Remove(node1.Position);
                OnNodeRemoved(node1);
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

        private void OnNodeAdded(Node node)
        {
            if (NodeAdded != null)
            {
                NodeAdded(node);
            }
        }

        private void OnNodeRemoved(Node node)
        {
            if (NodeRemoved != null)
            {
                NodeRemoved(node);
            }
        }

        private void OnConnectionAdded(NodeConnection connection)
        {
            if (ConnectionAdded != null)
            {
                ConnectionAdded(connection);
            }
        }

        private void OnConnectionRemoved(NodeConnection connection)
        {
            if (ConnectionRemoved != null)
            {
                ConnectionRemoved(connection);
            }
        }
    }
}