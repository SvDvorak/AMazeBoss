using System.Linq;
using Entitas;

namespace Assets.EntitasRefactor
{
    public static class PoolExtensions
    {
        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            return GetEntityAt(pool, position, Matcher.Tile);
        }

        public static Entity GetItemAt(this Pool pool, TilePos position)
        {
            return GetEntityAt(pool, position, Matcher.Item);
        }

        public static bool CanMoveTo(this Pool pool, TilePos position)
        {
            return GetEntityAt(pool, position, Matcher.Walkable) != null;
        }

        private static Entity GetEntityAt(Pool pool, TilePos position, IMatcher entityMatcher)
        {
            var entities = pool.GetEntities(Matcher.AllOf(Matcher.Position, entityMatcher));
            return entities.SingleOrDefault(x => x.position.Value == position);
        }

        public static void Clear(this Pool pool, IMatcher matcher)
        {
            foreach (var entity in pool.GetEntities(matcher))
            {
                entity.IsDestroyed(true);
            }
        }
    }
}