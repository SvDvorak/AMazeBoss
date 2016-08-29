using System;
using System.Collections.Generic;
using Assets.Features.Level;
using Assets.Level;
using Assets.Level.Editor_;

namespace Assets.Editor.Undo
{
    public class RemoveNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;
        private readonly List<NodeConnection> _removedConnections = new List<NodeConnection>();
        private readonly List<PuzzleObject> _removedObjects = new List<PuzzleObject>();
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
            _layout.ObjectRemoved += AddRemovedObject;

            _layout.RemoveNodeConnection(_connection);

            _layout.ObjectRemoved -= AddRemovedObject;
            _layout.ConnectionRemoved -= AddRemovedConnection;
        }

        private void AddRemovedConnection(NodeConnection connection)
        {
            _removedConnections.Add(connection);
        }

        private void AddRemovedObject(PuzzleObject puzzleObject)
        {
            _removedObjects.Add(puzzleObject);
        }

        public void Undo()
        {
            foreach (var removedConnection in _removedConnections)
            {
                _layout.AddNodeConnections(removedConnection);
            }

            foreach (var removedObject in _removedObjects)
            {
               _layout.PlaceObject(removedObject.Type, removedObject.Position); 
            }
        }
    }
}