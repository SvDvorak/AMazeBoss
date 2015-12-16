using System;

namespace Assets.FileOperations
{
    [Serializable]
    public class FileTile
    {
        public MainTileType MainType;
        public string Subtype;
        public int X;
        public int Z;
        public int Rotation;

        public FileTile(MainTileType mainType, string subtype, int x, int z, int rotation)
        {
            Rotation = rotation;
            MainType = mainType;
            Subtype = subtype;
            X = x;
            Z = z;
        }
    }
}