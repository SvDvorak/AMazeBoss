using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.FileOperations
{
    [Serializable]
    public class FileObject
    {
        public string Class;
        public string MainType;
        public string Subtype;
        public int X;
        public int Z;
        public int Rotation;
        public string Descriptors;

        public FileObject(string mainType, string subtype, int x, int z, int rotation, IEnumerable<string> descriptors)
        {
            Rotation = rotation;
            MainType = mainType;
            Subtype = subtype;
            X = x;
            Z = z;
            Descriptors = string.Join(";", descriptors.ToArray());
        }
    }
}