using Assets.LevelEditorUnity;

namespace Assets.Editor.Undo
{
    public class PlaceNormalObjectCommand : PlaceObjectCommand
    {
        public override string Name { get { return "Added object"; } }

        public PlaceNormalObjectCommand(PuzzleLayout layout, string type, TilePos position) : base(layout, type, position)
        {
        }

        protected override void ExecutePlace()
        {
            Layout.PlaceObject(Type, Position);
        }

        protected override void UndoPlace()
        {
            Layout.RemoveObject(Position);
        }
    }
}