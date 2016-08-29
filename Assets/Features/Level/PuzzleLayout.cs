using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Assets.Level.Editor_;

namespace Assets.Level
{
    public class PuzzleObject
    {
        public readonly string Type;
        public readonly TilePos Position;
        public readonly Dictionary<string, string> Properties;

        public PuzzleObject(string type, TilePos position)
        {
            Position = position;
            Type = type;
            Properties = new Dictionary<string, string>();
        }
    }

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
            set { _instance = value; }
        }

        public readonly Dictionary<TilePos, Node> Nodes = new Dictionary<TilePos, Node>();

        public event Action LayoutChanged;

        public event Action<Node> NodeAdded;
        public event Action<Node> NodeRemoved;
        public event Action<NodeConnection> ConnectionAdded;
        public event Action<NodeConnection> ConnectionRemoved;

        private readonly Dictionary<TilePos, PuzzleObject> _puzzleObjects = new Dictionary<TilePos, PuzzleObject>();

        public event Action<string, TilePos> ObjectAdded;
        public event Action<PuzzleObject> ObjectRemoved;

        public event Action<TilePos, string, object> PropertySet;

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

            CallLayoutChanged(() => ConnectionAdded.CallEvent(connection));
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

            CallLayoutChanged(() => ConnectionRemoved.CallEvent(connection));
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

        public void SetSingleton(string type, TilePos position)
        {
            var existingObject = _puzzleObjects.Values.SingleOrDefault(x => x.Type == type);
            if (existingObject != null)
            {
                RemoveObject(existingObject.Position);
            }

            PlaceObject(type, position);
        }

        public void PlaceObject(string type, TilePos position, Dictionary<string, string> properties = null)
        {
            RemoveObject(position);

            _puzzleObjects[position] = new PuzzleObject(type, position);

            if (properties != null)
            {
                properties.ToList().ForEach(x => SetProperty(position, x.Key, x.Value));
            }

            CallLayoutChanged(() => ObjectAdded.CallEvent(type, position));
        }

        public void RemoveObject(TilePos position)
        {
            var currentAtSamePosition = GetObjectAt(position);

            if (currentAtSamePosition != null)
            {
                _puzzleObjects.Remove(position);

                CallLayoutChanged(() => ObjectRemoved.CallEvent(currentAtSamePosition));
            }
        }

        public PuzzleObject GetObjectAt(TilePos position)
        {
            return _puzzleObjects.ContainsKey(position) ? _puzzleObjects[position] : null;
        }

        public PuzzleObject GetSingleton(string type)
        {
            return _puzzleObjects.Values.SingleOrDefault(x => x.Type == type);
        }

        public List<PuzzleObject> GetObjects(string type)
        {
            return GetAllObjects().Where(x => x.Type == type).ToList();
        }

        public List<PuzzleObject> GetAllObjects()
        {
            return _puzzleObjects.Values.ToList();
        }

        public bool CanPlaceAt(TilePos position)
        {
            return Nodes.ContainsKey(position);
        }

        private void CallLayoutChanged(Action action)
        {
            action();
            LayoutChanged.CallEvent();
        }

        public void SetProperty(TilePos position, string key, object value)
        {
            try
            {
                var objectToSetProperty = GetObjectAt(position);
                objectToSetProperty.Properties[key] = value.ToString();
                CallLayoutChanged(() => PropertySet.CallEvent(position, key, value));
            }
            catch (Exception)
            {
                throw new ObjectNotFoundException();
            }
        }

        public void RemoveProperty(TilePos position, string key)
        {
            try
            {
                var objectToSetProperty = GetObjectAt(position);
                objectToSetProperty.Properties.Remove(key);
                CallLayoutChanged(() => { });
            }
            catch (NullReferenceException)
            {
                throw new ObjectNotFoundException();
            }
        }

        public bool HasProperty(TilePos position, string key)
        {
            var puzzleObject = GetObjectAt(position);
            return puzzleObject != null && puzzleObject.Properties.ContainsKey(key);
        }

        public string GetProperty(TilePos position, string key)
        {
            try
            {
                return GetObjectAt(position).Properties[key];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception(string.Format("Could not find property {0} at {1}", key, position));
            }
        }

        public T GetProperty<T>(TilePos position, string key)
        {
            try
            {
                var property = GetProperty(position, key);

                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(property);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to get {0} as {1} because {2}",
                    key,
                    typeof(T),
                    ex.Message));
            }
        }
    }

    public class ObjectNotFoundException : Exception
    {
    }
}