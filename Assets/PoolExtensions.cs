using System;
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
            var hero = pool.GetEntities(GameMatcher.Hero).SingleEntity();
            var closestBoss = pool.GetActiveBoss(hero);

            hero.isCursed = !hero.isCursed;
            closestBoss.isCursed = !closestBoss.isCursed;
        }

        public static Entity GetActiveBoss(this Pool pool, Entity hero)
        {
            return pool
                .GetEntities(GameMatcher.Boss)
                .OrderBy(x => (x.position.Value - hero.position.Value).ManhattanDistance())
                .First();
        }

        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, x => x.isTile);
        }

        public static Entity GetItemAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, x => x.isItem);
        }

        public static Entity GetAreaAt(this Pool pool, TilePos position)
        {
            return pool.GetEntityAt(position, x => x.isArea);
        }

        public static void KnockObjectsInFront(this Pool pool, TilePos position, TilePos forwardDirection, bool immediate)
        {
            pool.GetEntitiesAt(position + forwardDirection, x => x.isItem && x.isBlockingTile)
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
            var entitiesAtPosition = pool.GetEntitiesAt(position, x => x.isBox).ToList();
            return entitiesAtPosition.Count > 0;
        }

        public static void SafeDeleteAll(this Pool pool, IMatcher matcher = null)
        {
            var entities = matcher != null ? pool.GetEntities(matcher) : pool.GetEntities();
            entities.DoForAll(x => x.IsDestroyed(true));
        }

        public static void SafeDeleteLevel(this Pool pool)
        {
            Pools.game.SafeDeleteAll(Matcher.AnyOf(GameMatcher.Tile, GameMatcher.Item, GameMatcher.Area));
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

        public static Entity[] DoForAll(this Entity[] entities, Action<Entity> action)
        {
            foreach (var entity in entities)
            {
                action(entity);
            }
            return entities;
        }
    }
}