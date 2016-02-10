using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Levels levels { get { return (Assets.Levels)GetComponent(GameComponentIds.Levels); } }

        public bool hasLevels { get { return HasComponent(GameComponentIds.Levels); } }

        static readonly Stack<Assets.Levels> _levelsComponentPool = new Stack<Assets.Levels>();

        public static void ClearLevelsComponentPool() {
            _levelsComponentPool.Clear();
        }

        public Entity AddLevels(System.Collections.Generic.List<string> newValue) {
            var component = _levelsComponentPool.Count > 0 ? _levelsComponentPool.Pop() : new Assets.Levels();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Levels, component);
        }

        public Entity ReplaceLevels(System.Collections.Generic.List<string> newValue) {
            var previousComponent = hasLevels ? levels : null;
            var component = _levelsComponentPool.Count > 0 ? _levelsComponentPool.Pop() : new Assets.Levels();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Levels, component);
            if (previousComponent != null) {
                _levelsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLevels() {
            var component = levels;
            RemoveComponent(GameComponentIds.Levels);
            _levelsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity levelsEntity { get { return GetGroup(GameMatcher.Levels).GetSingleEntity(); } }

        public Assets.Levels levels { get { return levelsEntity.levels; } }

        public bool hasLevels { get { return levelsEntity != null; } }

        public Entity SetLevels(System.Collections.Generic.List<string> newValue) {
            if (hasLevels) {
                throw new EntitasException("Could not set levels!\n" + this + " already has an entity with Assets.Levels!",
                    "You should check if the pool already has a levelsEntity before setting it or use pool.ReplaceLevels().");
            }
            var entity = CreateEntity();
            entity.AddLevels(newValue);
            return entity;
        }

        public Entity ReplaceLevels(System.Collections.Generic.List<string> newValue) {
            var entity = levelsEntity;
            if (entity == null) {
                entity = SetLevels(newValue);
            } else {
                entity.ReplaceLevels(newValue);
            }

            return entity;
        }

        public void RemoveLevels() {
            DestroyEntity(levelsEntity);
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherLevels;

        public static IMatcher Levels {
            get {
                if (_matcherLevels == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Levels);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherLevels = matcher;
                }

                return _matcherLevels;
            }
        }
    }
