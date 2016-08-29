using Assets.Features.Level;
using Assets.Level;

namespace Assets.Editor.Undo
{
    public class RemoveObjectCommand : ICommand
    {
        private readonly PuzzleLayout _layout;
        private readonly TilePos _position;
        private readonly PuzzleObject _previousPuzzleObject;

        public string Name { get { return "Added object"; } }

        public RemoveObjectCommand(PuzzleLayout layout, TilePos position)
        {
            _position = position;
            _layout = layout;
            _previousPuzzleObject = _layout.GetObjectAt(_position);
        }

        public void Execute()
        {
            _layout.RemoveObject(_position);
        }

        public void Undo()
        {
            _layout.PlaceObject(_previousPuzzleObject.Type, _position);
        }
    }
}
