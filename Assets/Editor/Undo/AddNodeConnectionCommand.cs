using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class AddNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;
        private readonly PuzzleLayout _layout;

        public string Name { get { return "Added node connection"; } }

        public AddNodeConnectionCommand(PuzzleLayout layout, NodeConnection connection)
        {
            _layout = layout;
            _connection = connection;
        }

        public void Execute()
        {
            _layout.AddNodeConnections(_connection);
        }

        public void Undo()
        {
            _layout.RemoveNodeConnection(_connection);
        }
    }
}