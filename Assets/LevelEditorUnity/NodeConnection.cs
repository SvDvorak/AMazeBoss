using System.Collections.Generic;

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
            return
                Start.Equals(other.Start) && End.Equals(other.End) ||
                Start.Equals(other.End) && End.Equals(other.Start);
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
                return Start.GetHashCode() + End.GetHashCode();
            }
        }

        public List<NodeConnection> GetSubdividedConnection()
        {
            var direction = End - Start;
            var subdivideCount = direction.Length();
            var directionNormalized = direction.Normalized();
            var affectedConnections = new List<NodeConnection>();

            for (int i = 0; i < subdivideCount; i++)
            {
                var subConnectionStart = Start + directionNormalized * i;
                var subConnectionEnd = Start + directionNormalized * (i + 1);
                var subConnection = new NodeConnection(subConnectionStart, subConnectionEnd);

                affectedConnections.Add(subConnection);
            }

            return affectedConnections;
        }
    }
}