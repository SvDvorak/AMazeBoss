using System;
using System.Collections.Generic;

namespace Assets.FileOperations
{
    [Serializable]
    public class MapObjects
    {
        public List<FileObject> Tiles;

        public MapObjects(List<FileObject> tiles)
        {
            Tiles = tiles;
        }
    }
}