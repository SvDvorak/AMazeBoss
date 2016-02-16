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
            var cameras = Pools.game.GetEntities(GameMatcher.Camera);
            return cameras.SingleEntity().camera.Value;
        }

        public static void SwitchCurse(this Pool pool)
        {
            var heroes = pool.GetEntities(GameMatcher.Hero);
            var bosses = pool.GetEntities(GameMatcher.Boss);

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
            return pool.GetEntityAt(position, GameMatcher.Item);
        }

        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, GameMatcher.Tile);
        }

        public static void KnockObjectsInFront(this Pool pool, TilePos position, TilePos forwardDirection, bool immediate)
        {
            pool.GetEntitiesAt(position + forwardDirection, GameMatcher.Item)
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
            var entitiesAtPosition = pool.GetEntitiesAt(position, GameMatcher.Box).ToList();
            return entitiesAtPosition.Count > 0;
        }

        public static IEnumerable<Entity> GetEntitiesAt(this Pool pool, TilePos position, IMatcher entityMatcher = null)
        {
            var completeMatcher = entityMatcher != null
                ? Matcher.AllOf(GameMatcher.Position, entityMatcher)
                : GameMatcher.Position;
            return pool.GetEntities(completeMatcher).Where(x => x.position.Value == position && !x.isDestroyed);
        }

        public static Entity GetEntityAt(this Pool pool, TilePos position, IMatcher entityMatcher = null)
        {
            return pool.GetEntitiesAt(position, entityMatcher).SingleOrDefault();
        }

        public static void SafeDeleteAll(this Pool pool, IMatcher matcher = null)
        {
            var entities = matcher != null ? pool.GetEntities(matcher) : pool.GetEntities();
            entities.DoForAll(x => x.IsDestroyed(true));
        }

        public static Entity FindChildFor(this Pool pool, Entity entity)
        {
            return pool.FindChildrenFor(entity).SingleEntity();
        }

        public static List<Entity> FindChildrenFor(this Pool pool, Entity entity)
        {
            return pool
                .GetEntities(GameMatcher.Child)
                .Where(x => x.child.ParentId == entity.id.Value)
                .ToList();
        }
    }
}