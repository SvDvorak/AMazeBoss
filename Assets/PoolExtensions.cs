using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public static class PoolExtensions
    {
        public static UnityEngine.Camera GetCamera(this Pool pool)
        {
            var cameras = Pools.pool.GetEntities(Matcher.Camera);
            return cameras.SingleEntity().camera.Value;
        }

        public static void SwitchCurse(this Pool pool)
        {
            var heroes = pool.GetEntities(Matcher.Hero);
            var bosses = pool.GetEntities(Matcher.Boss);

            foreach (var hero in heroes)
            {
                hero.isCursed = !hero.isCursed;
            }

            foreach (var boss in bosses)
            {
                boss.isCursed = !boss.isCursed;
            }
        }

        public static Entity GetItemAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, Matcher.Item);
        }

        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, Matcher.Tile);
        }

        public static void KnockObjectsInFront(this Pool pool, TilePos position, TilePos forwardDirection, bool immediate)
        {
            pool.GetEntitiesAt(position + forwardDirection, Matcher.Item)
                .Where(x => x.isBlockingTile)
                .ToList()
                .ForEach(x => x.ReplaceKnocked(forwardDirection, immediate));
        }

        public static bool OpenTileAt(this Pool pool, TilePos position)
        {
            var entitiesAtPosition = pool.GetEntitiesAt(position).ToList();
            return entitiesAtPosition.Count > 0 && entitiesAtPosition.All(x => !x.isBlockingTile);
        }

        public static bool PushableItemAt(this Pool pool, TilePos position)
        {
            var entitiesAtPosition = pool.GetEntitiesAt(position, Matcher.Box).ToList();
            return entitiesAtPosition.Count > 0;
        }

        public static IEnumerable<Entity> GetSurroundingEntities(
            this Pool pool,
            TilePos centerPosition,
            IMatcher entityMatcher)
        {
            return pool
                .GetEntities(entityMatcher)
                .Where(x =>
                    !x.isDestroyed &&
                    (x.position.Value - centerPosition).ManhattanDistance() == 1);
        }

        public static IEnumerable<Entity> GetEntitiesAt(this Pool pool, TilePos position, IMatcher entityMatcher = null)
        {
            var completeMatcher = entityMatcher != null
                ? Matcher.AllOf(Matcher.Position, entityMatcher)
                : Matcher.Position;
            return pool.GetEntities(completeMatcher).Where(x => x.position.Value == position && !x.isDestroyed);
        }

        public static Entity GetEntityAt(this Pool pool, TilePos position, IMatcher entityMatcher = null)
        {
            return pool.GetEntitiesAt(position, entityMatcher).SingleOrDefault();
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
                .Where(x => x.child.ParentId == entity.id.Value)
                .ToList();
        }
    }
}