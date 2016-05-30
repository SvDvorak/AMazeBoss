using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class RemoveNodeConnectionCommand : ICommand
    {
        private readonly NodeConnection _connection;

        public string Name { get { return "Added node connection"; } }

        public RemoveNodeConnectionCommand(NodeConnection connection)
        {
            _connection = connection;
        }

        public void Execute()
        {
            PuzzleLayout.Instance.RemoveNodeConnection(_connection);
        }

        public void Undo()
        {
            PuzzleLayout.Instance.AddNodeConnection(_connection);
        }
    }
}