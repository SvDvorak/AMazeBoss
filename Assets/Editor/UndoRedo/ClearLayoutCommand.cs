using System.Collections.Generic;
using Assets.Features.Level;
using Assets.Level;
using Assets.Level.Editor_;

namespace Assets.Editor.Undo
{
    public class ClearLayoutCommand : ICommand
    {
        private readonly List<NodeConnection> _currentConnections;
        private readonly PuzzleLayout _layout;

        public string Name { get { return "Clear Layout"; } }

        public ClearLayoutCommand(PuzzleLayout layout)
        {
            _layout = layout;
            _currentConnections = layout.GetAllConnections();
        }

        public void Execute()
        {
            _layout.Clear();
        }

        public void Undo()
        {
            foreach (var connection in _currentConnections)
            {
                _layout.AddNodeConnections(connection);
            }
        }
    }
}