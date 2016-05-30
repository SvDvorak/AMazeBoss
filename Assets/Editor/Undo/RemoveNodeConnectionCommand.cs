using System.Collections.Generic;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class RemoveNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;
        private List<NodeConnection> _removedConnections = new List<NodeConnection>();

        public string Name { get { return "Added node connection"; } }

        public RemoveNodeConnectionCommand(NodeConnection connection)
        {
            _connection = connection;
        }

        public void Execute()
        {
            _removedConnections = PuzzleLayout.Instance.RemoveAndReturnNodeConnections(_connection);
        }

        public void Undo()
        {
            foreach (var removedConnection in _removedConnections)
            {
                PuzzleLayout.Instance.AddNodeConnections(removedConnection);
            }
        }
    }
}