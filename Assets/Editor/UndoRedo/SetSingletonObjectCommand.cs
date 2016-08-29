using Assets.Features.Level;
using Assets.Level;

namespace Assets.Editor.Undo
{
    public class SetSingletonObjectCommand : PlaceObjectCommand
    {
        private readonly PuzzleObject _previousPuzzleObject;

        public override string Name { get { return "Added singleton"; } }

        public SetSingletonObjectCommand(PuzzleLayout layout, string type, TilePos position) : base(layout, type, position)
        {
            _previousPuzzleObject = layout.GetSingleton(type);
        }

        protected override void ExecutePlace()
        {
            Layout.SetSingleton(Type, Position);
        }

        protected override void UndoPlace()
        {
            if (_previousPuzzleObject != null)
            {
                Layout.SetSingleton(Type, _previousPuzzleObject.Position);
            }
        }
    }
}
