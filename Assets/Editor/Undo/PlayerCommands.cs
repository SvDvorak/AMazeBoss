using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class AddPlayerCommand : ICommand
    {
        private readonly TilePos _position;
        private readonly PuzzleLayout _layout;
        private readonly TilePos? _previousPosition;

        public string Name { get { return "Added player"; } }

        public AddPlayerCommand(PuzzleLayout layout, TilePos position)
        {
            _layout = layout;
            _position = position;
            _previousPosition = layout.PlayerPosition;
        }

        public void Execute()
        {
            _layout.PlayerPosition = _position;
        }

        public void Undo()
        {
            _layout.PlayerPosition = _previousPosition;
        }
    }

    public class RemovePlayerCommand : ICommand
    {
        private readonly TilePos? _previousPosition;
        private readonly PuzzleLayout _layout;

        public string Name { get { return "Removed player"; } }

        public RemovePlayerCommand(PuzzleLayout layout)
        {
            _layout = layout;
            _previousPosition = layout.PlayerPosition;
        }

        public void Execute()
        {
            _layout.PlayerPosition = null;
        }

        public void Undo()
        {
            _layout.PlayerPosition = _previousPosition;
        }
    }
}
