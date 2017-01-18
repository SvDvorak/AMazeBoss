using System;
using System.Collections.Generic;
using Assets.Level.Editor_;

namespace Assets.Features.Level
{
    public class PuzzleObject
    {
        public readonly string Type;
        public readonly TilePos Position;
        public readonly Dictionary<string, Property> Properties;

        public PuzzleObject(string type, TilePos position)
        {
            Position = position;
            Type = type;
            Properties = new Dictionary<string, Property>();
        }

        public class Property
        {
            public readonly string Key;
            public readonly object Value;
            public readonly Type Type;

            public Property(string key, object value)
            {
                Key = key;
                Value = value;
                Type = value.GetType();
            }
        }
    }

    public class PuzzleLayout
    {
        private static PuzzleLayout _instance;
        public static PuzzleLayout Instance
        {
            get
            {
                if (PuzzleLayout._instance == null)
                {
                    PuzzleLayout._instance = new PuzzleLayout();
                }
                return PuzzleLayout._instance;
            }
            set { PuzzleLayout._instance = value; }
        }

        public Dictionary<TilePos, Node> Nodes { get { return _puzzleNodes.Nodes; } }

        public event Action LayoutChanged;

        public event Action<Node> NodeAdded { add { _puzzleNodes.NodeAdded += value; } remove { _puzzleNodes.NodeAdded -= value; } }
        public event Action<Node> NodeRemoved { add { _puzzleNodes.NodeRemoved += value; } remove { _puzzleNodes.NodeRemoved -= value; } }
        public event Action<NodeConnection> ConnectionAdded { add { _puzzleNodes.ConnectionAdded += value; } remove { _puzzleNodes.ConnectionAdded -= value; } }
        public event Action<NodeConnection> ConnectionRemoved { add { _puzzleNodes.ConnectionRemoved += value; } remove { _puzzleNodes.ConnectionRemoved -= value; } }

        public event Action<string, TilePos> ObjectAdded { add { _puzzleObjects.ObjectAdded += value; } remove { _puzzleObjects.ObjectAdded -= value; } }
        public event Action<PuzzleObject> ObjectRemoved { add { _puzzleObjects.ObjectRemoved += value; } remove { _puzzleObjects.ObjectRemoved -= value; } }
        public event Action<TilePos, string, object> PropertySet { add { _puzzleObjects.PropertySet += value; } remove { _puzzleObjects.PropertySet -= value; } }

        private readonly PuzzleNodes _puzzleNodes;
        private readonly PuzzleObjects _puzzleObjects;

        public PuzzleLayout()
        {
            _puzzleNodes = new PuzzleNodes();
            _puzzleObjects = new PuzzleObjects();

            NodeRemoved += node => RemoveObject(node.Position);
            ConnectionAdded += _ => LayoutChanged.CallEvent();
            ConnectionRemoved += _ => LayoutChanged.CallEvent();

            ObjectAdded += (_, __) => LayoutChanged.CallEvent();
            ObjectRemoved += _ => LayoutChanged.CallEvent();
            PropertySet += (_, __, ___) => LayoutChanged.CallEvent();
            _puzzleObjects.PropertyRemoved += (_, __) => LayoutChanged.CallEvent();
        }

        public void Clear()
        {
            _puzzleNodes.Clear();
        }

        public void AddNodeConnections(NodeConnection connection)
        {
            _puzzleNodes.AddNodeConnections(connection);
        }

        public void RemoveNodeConnection(NodeConnection connection)
        {
            _puzzleNodes.RemoveNodeConnection(connection);
        }

        public List<NodeConnection> GetAllConnections()
        {
            return _puzzleNodes.GetAllConnections();
        }

        public bool CanPlaceAt(TilePos position)
        {
            return _puzzleNodes.CanPlaceAt(position);
        }

        public void PlaceObject(string type, TilePos position, Dictionary<string, object> properties = null)
        {
            _puzzleObjects.PlaceObject(type, position, properties);
        }

        public void RemoveObject(TilePos position)
        {
            _puzzleObjects.RemoveObject(position);
        }

        public PuzzleObject GetObjectAt(TilePos position)
        {
            return _puzzleObjects.GetObjectAt(position);
        }

        public PuzzleObject GetSingleton(string type)
        {
            return _puzzleObjects.GetSingleton(type);
        }

        public void SetSingleton(string type, TilePos position)
        {
            _puzzleObjects.SetSingleton(type, position);
        }

        public List<PuzzleObject> GetObjects(string type)
        {
            return _puzzleObjects.GetObjects(type);
        }

        public List<PuzzleObject> GetAllObjects()
        {
            return _puzzleObjects.GetAllObjects();
        }

        public PuzzleObject.Property GetProperty(TilePos position, string key)
        {
            return _puzzleObjects.GetProperty(position, key);
        }

        public void SetProperty(TilePos position, string key, object value)
        {
            _puzzleObjects.SetProperty(position, key, value);
        }

        public bool HasProperty(TilePos position, string key)
        {
            return _puzzleObjects.HasProperty(position, key);
        }

        public void RemoveProperty(TilePos position, string key)
        {
            _puzzleObjects.RemoveProperty(position, key);
        }
    }
}