using UnityEngine;

namespace Assets
{
    public struct TilePos
    {
        private const int TileLength = 2;

        public TilePos(Vector3 pos) : this(Mathf.RoundToInt(pos.x/TileLength), Mathf.RoundToInt(pos.z/TileLength))
        {
        }

        public TilePos(int x, int z)
        {
            X = x;
            Z = z;
        }

        public int X { get; private set; }
        public int Z { get; private set; }

        public static TilePos operator +(TilePos pos1, TilePos pos2)
        {
            return new TilePos(pos1.X + pos2.X, pos1.Z + pos2.Z);
        }

        public static TilePos operator -(TilePos pos1, TilePos pos2)
        {
            return new TilePos(pos1.X - pos2.X, pos1.Z - pos2.Z);
        }

        public static TilePos operator /(TilePos pos, int divider)
        {
            return new TilePos(pos.X/divider, pos.Z/divider);
        }

        public static bool operator ==(TilePos pos1, TilePos pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(TilePos pos1, TilePos pos2)
        {
            return !pos1.Equals(pos2);
        }

        public Vector3 ToV3()
        {
            return new Vector3(X, 0, Z)*TileLength;
        }

        public bool Equals(TilePos other)
        {
            return X == other.X && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is TilePos && Equals((TilePos) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Z;
            }
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Z: {1}", X, Z);
        }

        public float Length()
        {
            return Mathf.Sqrt(X*X + Z*Z);
        }

        public int ManhattanDistance()
        {
            return Mathf.Abs(X) + Mathf.Abs(Z);
        }

        public TilePos Rotate(int rotation)
        {
            var rotated = Quaternion.AngleAxis(rotation*90, Vector3.up)*ToV3();
            return new TilePos(rotated);
        }
    }
}