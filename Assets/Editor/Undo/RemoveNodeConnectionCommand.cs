using System;
using System.Collections.Generic;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class RemoveNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;
        private readonly List<NodeConnection> _removedConnections = new List<NodeConnection>();
        private readonly List<WorldObject> _removedObjects = new List<WorldObject>();
        private readonly PuzzleLayout _layout;

        public string Name { get { return "Added node connection"; } }

        public RemoveNodeConnectionCommand(PuzzleLayout layout, NodeConnection connection)
        {
            _layout = layout;
            _connection = connection;
        }

        public void Execute()
        {
            _layout.ConnectionRemoved += AddRemovedConnection;
            _layout.SingletonRemoved += AddRemovedObject;

            _layout.RemoveNodeConnection(_connection);

            _layout.SingletonRemoved -= AddRemovedObject;
            _layout.ConnectionRemoved -= AddRemovedConnection;
        }

        private void AddRemovedConnection(NodeConnection connection)
        {
            _removedConnections.Add(connection);
        }

        private void AddRemovedObject(string type, TilePos? position)
        {
            _removedObjects.Add(new WorldObject(type, position));
        }

        public void Undo()
        {
            foreach (var removedConnection in _removedConnections)
            {
                _layout.AddNodeConnections(removedConnection);
            }

            foreach (var removedObject in _removedObjects)
            {
               _layout.SetSingleton(removedObject.Type, removedObject.Position); 
            }
        }
    }
}