namespace Assets.Editor.Undo
{
    public class ChangedObject
    {
        public readonly string Type;
        public readonly TilePos Position;

        public ChangedObject(string type, TilePos position)
        {
            Position = position;
            Type = type;
        }
    }
}