using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class AddNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;

        public string Name { get { return "Added node connection"; } }

        public AddNodeConnectionCommand(NodeConnection connection)
        {
            _connection = connection;
        }

        public void Execute()
        {
            PuzzleLayout.Instance.AddNodeConnections(_connection);
        }

        public void Undo()
        {
            PuzzleLayout.Instance.RemoveAndReturnNodeConnections(_connection);
        }
    }
}