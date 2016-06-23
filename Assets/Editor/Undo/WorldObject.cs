namespace Assets.Editor.Undo
{
    public class WorldObject
    {
        public readonly string Type;
        public readonly TilePos Position;

        public WorldObject(string type, TilePos position)
        {
            Position = position;
            Type = type;
        }
    }
}