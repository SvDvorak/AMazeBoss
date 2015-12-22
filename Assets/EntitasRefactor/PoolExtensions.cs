using System.Linq;
using Entitas;

namespace Assets.EntitasRefactor
{
    public static class PoolExtensions
    {
        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            return GetEntityAt(pool, position, Matcher.AllOf(Matcher.Tile, Matcher.Position));
        }

        public static Entity GetItemAt(this Pool pool, TilePos position)
        {
            return GetEntityAt(pool, position, Matcher.AllOf(Matcher.Item, Matcher.Position));
        }

        private static Entity GetEntityAt(Pool pool, TilePos position, IMatcher entityMatcher)
        {
            var entities = pool.GetEntities(entityMatcher);
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