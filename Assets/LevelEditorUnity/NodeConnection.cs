namespace Assets.LevelEditorUnity
{
    public struct NodeConnection
    {
        public TilePos Start { get; private set; }
        public TilePos End { get; private set; }

        public NodeConnection(TilePos start, TilePos end)
        {
            Start = start;
            End = end;
        }

        public bool Equals(NodeConnection other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NodeConnection && Equals((NodeConnection) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode()*397) ^ End.GetHashCode();
            }
        }
    }
}