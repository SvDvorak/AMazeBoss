using System.Collections.Generic;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class RemoveNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;
        private List<NodeConnection> _removedConnections = new List<NodeConnection>();
        private readonly PuzzleLayout _layout;

        public string Name { get { return "Added node connection"; } }

        public RemoveNodeConnectionCommand(PuzzleLayout layout, NodeConnection connection)
        {
            _layout = layout;
            _connection = connection;
        }

        public void Execute()
        {
            _removedConnections = _layout.RemoveAndReturnNodeConnections(_connection);
        }

        public void Undo()
        {
            foreach (var removedConnection in _removedConnections)
            {
                _layout.AddNodeConnections(removedConnection);
            }
        }
    }
}