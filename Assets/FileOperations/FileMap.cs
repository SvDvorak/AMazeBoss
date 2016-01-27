using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.FileOperations
{
    [Serializable]
    public class FileMap
    {
        public FileCamera Camera;
        public List<FileMapObject> Tiles;

        public FileMap(FileCamera camera, List<FileMapObject> tiles)
        {
            Camera = camera;
            Tiles = tiles;
        }
    }

    [Serializable]
    public class FileCamera
    {
        public float FocusX;
        public float FocusZ;

        public FileCamera(float focusX, float focusZ)
        {
            FocusX = focusX;
            FocusZ = focusZ;
        }
    }

    [Serializable]
    public class FileMapObject
    {
        public string Class;
        public string MainType;
        public string Subtype;
        public int X;
        public int Z;
        public int Rotation;
        public string Descriptors;

        public FileMapObject(string mainType, string subtype, int x, int z, int rotation, IEnumerable<string> descriptors)
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