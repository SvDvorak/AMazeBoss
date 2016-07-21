using System;
using UnityEngine;

namespace Assets
{
    [Serializable]
    public struct TilePos
    {
        public const int TileLength = 2;

        public TilePos(Vector3 pos) : this(pos.x, pos.z)
        {
        }

        public TilePos(float x, float y) : this(Mathf.RoundToInt(x / TileLength), Mathf.RoundToInt(y / TileLength))
        {
        }

        public TilePos(int x, int z)
        {
            X = x;
            Z = z;
        }

        [SerializeField]
        public int X;
        [SerializeField]
        public int Z;

        public static TilePos operator +(TilePos pos1, TilePos pos2)
        {
            return new TilePos(pos1.X + pos2.X, pos1.Z + pos2.Z);
        }

        public static TilePos operator -(TilePos pos1, TilePos pos2)
        {
            return new TilePos(pos1.X - pos2.X, pos1.Z - pos2.Z);
        }

        public static TilePos operator -(TilePos pos)
        {
            return new TilePos(-pos.X, -pos.Z);
        }

        public static TilePos operator /(TilePos pos, int divider)
        {
            return new TilePos(pos.X/divider, pos.Z/divider);
        }

        public static TilePos operator *(TilePos pos, int multiplier)
        {
            return new TilePos(pos.X * multiplier, pos.Z * multiplier);
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

        public int Length()
        {
            return Mathf.RoundToInt(Mathf.Sqrt(X*X + Z*Z));
        }

        public int ManhattanDistance()
        {
            return Mathf.Abs(X) + Mathf.Abs(Z);
        }

        public TilePos Normalized()
        {
            return new TilePos(Mathf.Clamp(X, -1, 1), Mathf.Clamp(Z, -1, 1));
        }

        public TilePos Rotate(int rotation)
        {
            var rotated = Quaternion.AngleAxis(rotation*90, Vector3.up)*ToV3();
            return new TilePos(rotated);
        }

        public int RotationDifference(TilePos otherDirection)
        {
            return Mathf.Max(Mathf.Abs(X - otherDirection.X), Mathf.Abs(Z - otherDirection.Z));
        }
    }
}