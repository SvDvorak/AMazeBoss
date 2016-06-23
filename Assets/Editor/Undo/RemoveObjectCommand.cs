using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class RemoveObjectCommand : ICommand
    {
        private readonly PuzzleLayout _layout;
        private readonly TilePos _position;
        private readonly string _previousType;

        public string Name { get { return "Added object"; } }

        public RemoveObjectCommand(PuzzleLayout layout, TilePos position)
        {
            _position = position;
            _layout = layout;
            _previousType = _layout.GetObjectAt(_position);
        }

        public void Execute()
        {
            _layout.RemoveObject(_position);
        }

        public void Undo()
        {
            _layout.PlaceObject(_previousType, _position);
        }
    }
}
