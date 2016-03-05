using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelsComponent levels { get { return (Assets.LevelsComponent)GetComponent(GameComponentIds.Levels); } }

        public bool hasLevels { get { return HasComponent(GameComponentIds.Levels); } }

        public Entity AddLevels(System.Collections.Generic.List<string> newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Levels);
            var component = (Assets.LevelsComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelsComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Levels, component);
        }

        public Entity ReplaceLevels(System.Collections.Generic.List<string> newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Levels);
            var component = (Assets.LevelsComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelsComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Levels, component);
            return this;
        }

        public Entity RemoveLevels() {
            return RemoveComponent(GameComponentIds.Levels);;
        }
    }

    public partial class Pool {
        public Entity levelsEntity { get { return GetGroup(GameMatcher.Levels).GetSingleEntity(); } }

        public Assets.LevelsComponent levels { get { return levelsEntity.levels; } }

        public bool hasLevels { get { return levelsEntity != null; } }

        public Entity SetLevels(System.Collections.Generic.List<string> newValue) {
            if (hasLevels) {
                throw new EntitasException("Could not set levels!\n" + this + " already has an entity with Assets.LevelsComponent!",
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
