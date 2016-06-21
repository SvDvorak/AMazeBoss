using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class SetSingletonObjectCommand : ICommand
    {
        private readonly TilePos? _position;
        private readonly PuzzleLayout _layout;
        private readonly TilePos? _previousPosition;
        private readonly string _type;

        public string Name { get { return (_position != null ? "Added" : "Removed") + " player"; } }

        public SetSingletonObjectCommand(PuzzleLayout layout, string type, TilePos? position)
        {
            _type = type;
            _layout = layout;
            _position = position;
            _previousPosition = layout.GetSingleton(type);
        }

        public void Execute()
        {
            _layout.SetSingleton(_type, _position);
        }

        public void Undo()
        {
            _layout.SetSingleton(_type, _previousPosition);
        }
    }
}
