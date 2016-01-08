using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public static class PoolExtensions
    {
        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, Matcher.Tile);
        }

        public static Entity GetItemAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, Matcher.Item);
        }

        public static bool CanMoveTo(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, Matcher.Walkable) != null;
        }

        public static Entity GetEntityAt(this Pool pool, TilePos position, IMatcher entityMatcher)
        {
            var entities = pool.GetEntities(Matcher.AllOf(Matcher.Position, entityMatcher));
            return entities.SingleOrDefault(x => x.position.Value == position && !x.isDestroyed);
        }

        public static void Clear(this Pool pool, IMatcher matcher)
        {
            foreach (var entity in pool.GetEntities(matcher))
            {
                entity.IsDestroyed(true);
            }
        }

        public static Entity FindChildFor(this Pool pool, Entity entity)
        {
            return pool.FindChildrenFor(entity).SingleEntity();
        }

        public static List<Entity> FindChildrenFor(this Pool pool, Entity entity)
        {
            return pool
                .GetEntities(Matcher.Child)
                .Where(x => x.child.ParentId == entity.parent.Id)
                .ToList();
        }
    }
}