using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Level.Editor_
{
    [Serializable]
    public struct NodeConnection
    {
        [SerializeField]
        public TilePos Start;
        [SerializeField]
        public TilePos End;

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

        public int Length()
        {
            return (End - Start).ManhattanDistance();
        }

        public IEnumerable<NodeConnection> GetSubdividedConnection()
        {
            var direction = End - Start;
            var subdivideCount = direction.Length();
            var directionNormalized = direction.Normalized();

            for (int i = 0; i < subdivideCount; i++)
            {
                var subConnectionStart = Start + directionNormalized * i;
                var subConnectionEnd = Start + directionNormalized * (i + 1);
                var subConnection = new NodeConnection(subConnectionStart, subConnectionEnd);

                yield return subConnection;
            }
        }
    }
}