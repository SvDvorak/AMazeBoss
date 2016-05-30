using System.Collections.Generic;
using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class ClearLayoutCommand : ICommand
    {
        private readonly List<NodeConnection> _currentConnections;

        public string Name { get { return "Clear Layout"; } }

        public ClearLayoutCommand()
        {
            _currentConnections = PuzzleLayout.Instance.GetAllConnections();
        }

        public void Execute()
        {
            PuzzleLayout.Instance.Clear();
        }

        public void Undo()
        {
            foreach (var connection in _currentConnections)
            {
                PuzzleLayout.Instance.AddNodeConnection(connection);
            }
        }
    }
}