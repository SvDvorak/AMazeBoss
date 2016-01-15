using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.Levels levels { get { return (Assets.Levels)GetComponent(ComponentIds.Levels); } }

        public bool hasLevels { get { return HasComponent(ComponentIds.Levels); } }

        static readonly Stack<Assets.Levels> _levelsComponentPool = new Stack<Assets.Levels>();

        public static void ClearLevelsComponentPool() {
            _levelsComponentPool.Clear();
        }

        public Entity AddLevels(System.Collections.Generic.List<string> newValue) {
            var component = _levelsComponentPool.Count > 0 ? _levelsComponentPool.Pop() : new Assets.Levels();
            component.Value = newValue;
            return AddComponent(ComponentIds.Levels, component);
        }

        public Entity ReplaceLevels(System.Collections.Generic.List<string> newValue) {
            var previousComponent = hasLevels ? levels : null;
            var component = _levelsComponentPool.Count > 0 ? _levelsComponentPool.Pop() : new Assets.Levels();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Levels, component);
            if (previousComponent != null) {
                _levelsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLevels() {
            var component = levels;
            RemoveComponent(ComponentIds.Levels);
            _levelsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity levelsEntity { get { return GetGroup(Matcher.Levels).GetSingleEntity(); } }

        public Assets.Levels levels { get { return levelsEntity.levels; } }

        public bool hasLevels { get { return levelsEntity != null; } }

        public Entity SetLevels(System.Collections.Generic.List<string> newValue) {
            if (hasLevels) {
                throw new SingleEntityException(Matcher.Levels);
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

    public partial class Matcher {
        static IMatcher _matcherLevels;

        public static IMatcher Levels {
            get {
                if (_matcherLevels == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Levels);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherLevels = matcher;
                }

                return _matcherLevels;
            }
        }
    }
}
