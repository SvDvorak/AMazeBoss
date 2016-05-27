using System;
using System.Collections.Generic;

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
            var eitherNodeDoesntExistInLayout = !Nodes.ContainsKey(connection.Start) || !Nodes.ContainsKey(connection.End);
            if (eitherNodeDoesntExistInLayout)
            {
                return;
            }

            var node1 = Nodes[connection.Start];
            var node2 = Nodes[connection.End];
            RemoveConnection(node1, node2);
            RemoveConnection(node2, node1);

            OnConnectionRemoved(connection);
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

        private void RemoveConnection(Node node1, Node node2)
        {
            node1.Connections.Remove(node2);

            if (node1.Connections.Count == 0)
            {
                Nodes.Remove(node1.Position);
                OnNodeRemoved(node1);
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

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}