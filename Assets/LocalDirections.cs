using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class LocalDirections
    {
        private static readonly List<Tuple<TilePos, int>> DirectionRotation = new List<Tuple<TilePos, int>>()
            {
                new Tuple<TilePos, int>(new TilePos(0, 1), 0),
                new Tuple<TilePos, int>(new TilePos(1, 0), 1),
                new Tuple<TilePos, int>(new TilePos(0, -1), 2),
                new Tuple<TilePos, int>(new TilePos(-1, 0), 3)
            };

        public static int ToRotation(TilePos direction)
        {
            return DirectionRotation.Single(x => x.Item1 == direction).Item2;
        }

        public static TilePos ToDirection(int rotation)
        {
            var clampedRotation = rotation%4;
            if (clampedRotation < 0)
            {
                clampedRotation += 4;
            }
            return DirectionRotation.Single(x => x.Item2 == clampedRotation).Item1;
        }

        public static List<TilePos> GetAll()
        {
            return DirectionRotation.Select(x => x.Item1).ToList();
        } 
    }
}