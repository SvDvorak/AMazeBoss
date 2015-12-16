using System;
using System.Collections.Generic;

namespace Assets.FileOperations
{
    [Serializable]
    public class FileTiles
    {
        public List<FileTile> Tiles;

        public FileTiles(List<FileTile> tiles)
        {
            Tiles = tiles;
        }
    }
}