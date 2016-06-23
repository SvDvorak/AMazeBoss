using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class SetSingletonObjectCommand : PlaceObjectCommand
    {
        private readonly TilePos? _previousPosition;

        public override string Name { get { return "Added singleton"; } }

        public SetSingletonObjectCommand(PuzzleLayout layout, string type, TilePos position) : base(layout, type, position)
        {
            _previousPosition = layout.GetSingleton(type);
        }

        protected override void ExecutePlace()
        {
            Layout.SetSingleton(Type, Position);
        }

        protected override void UndoPlace()
        {
            Layout.SetSingleton(Type, _previousPosition);
        }
    }
}
