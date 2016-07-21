using System;
using Entitas;

namespace Assets.LevelEditor
{
    public class EntityPerformer
    {
        private readonly Action<Entity, Pool> _action;

        public EntityPerformer(Action<Entity, Pool> action)
        {
            _action = action;
        }

        public Entity Do(Entity entity, Pool pool)
        {
            _action(entity, pool);
            return entity;
        }
    }

    public static class WorldObjects
    {
        public static EntityPerformer Empty
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Tile)
                    .ReplaceMaintype(MainTileType.Normal.ToString()));
            }
        }

        public static EntityPerformer Pillar
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Tile)
                    .ReplaceMaintype(MainTileType.Pillar.ToString())
                    .IsBlockingTile(true));
            }
        }

        public static EntityPerformer Wall
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Tile)
                    .ReplaceMaintype(MainTileType.Wall.ToString())
                    .IsWall(true)
                    .IsBlockingTile(true));
            }
        }

        public static EntityPerformer SpikeTrap
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Tile)
                    .ReplaceMaintype(MainTileType.SpikeTrap.ToString())
                    .IsDynamic(true)
                    .IsSpikeTrap(true));
            }
        }

        public static EntityPerformer CurseTrigger
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Tile)
                    .ReplaceMaintype(MainTileType.CurseTrigger.ToString())
                    .IsDynamic(true)
                    .IsCurseSwitch(true));
            }
        }

        public static EntityPerformer WallTrap
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Item)
                    .ReplaceMaintype(ItemType.PillarTrap.ToString())
                    .IsDynamic(true)
                    .IsSpikeTrap(true));
            }
        }

        public static EntityPerformer Hero
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Item)
                    .ReplaceMaintype(ItemType.Hero.ToString())
                    .IsDynamic(true)
                    .IsCharacter(true)
                    .IsHero(true)
                    .ReplaceHealth(3));
            }
        }

        public static EntityPerformer Boss
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Item)
                    .ReplaceMaintype(ItemType.Boss.ToString())
                    .AddId(pool)
                    .IsDynamic(true)
                    .IsBlockingTile(true)
                    .IsCharacter(true)
                    .IsBoss(true)
                    .IsCursed(true)
                    .ReplaceHealth(3));
            }
        }

        public static EntityPerformer Spikes
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Item)
                    .ReplaceMaintype(ItemType.Spikes.ToString())
                    .IsSpikes(true));
            }
        }

        public static EntityPerformer Box
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Item)
                    .ReplaceMaintype(ItemType.Box.ToString())
                    .IsBlockingTile(true)
                    .IsDynamic(true)
                    .IsBox(true));
            }
        }

        public static EntityPerformer ExitGate
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Item)
                    .ReplaceMaintype(ItemType.ExitGate.ToString())
                    .IsDynamic(true)
                    .IsBlockingTile(true)
                    .AddExitGate(true));
            }
        }

        public static EntityPerformer ExitTrigger
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Area)
                    .ReplaceMaintype(AreaType.ExitTrigger.ToString())
                    .IsExitTrigger(true)
                    .ReplaceEditorOnlyVisual(ViewMode.Area));
            }
        }

        public static EntityPerformer PuzzleArea
        {
            get
            {
                return Action((e, pool) => e
                    .ReplaceGameObject(ObjectType.Area)
                    .ReplaceMaintype(AreaType.Puzzle.ToString())
                    .IsPuzzleArea(true)
                    .ReplaceEditorOnlyVisual(ViewMode.Area));
            }
        }

        private static EntityPerformer Action(Action<Entity, Pool> action)
        {
            return new EntityPerformer(action);
        }
    }
}