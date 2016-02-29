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
            var activeBoss = pool.GetActiveBoss(hero);

            if (activeBoss != null)
            {
                hero.isCursed = !hero.isCursed;
                activeBoss.isCursed = !activeBoss.isCursed;
            }
        }

        public static Entity GetActiveBoss(this Pool pool, Entity hero)
        {
            try
            {
                var currentPuzzleArea = pool.GetEntityAt(hero.position.Value, x => x.isPuzzleArea);
                return pool
                    .GetEntities(GameMatcher.Boss)
                    .Single(x => x.id.Value == currentPuzzleArea.bossConnection.BossId);
            }
            catch (Exception)
            {
                return null;
            }
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
            entities.ToList().DoForAll(x => x.IsDestroyed(true));
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

        public static List<Entity> DoForAll(this List<Entity> entities, Action<Entity> action)
        {
            foreach (var entity in entities)
            {
                action(entity);
            }
            return entities;
        }
    }
}