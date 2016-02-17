using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.KnockedComponent knocked { get { return (Assets.KnockedComponent)GetComponent(GameComponentIds.Knocked); } }

        public bool hasKnocked { get { return HasComponent(GameComponentIds.Knocked); } }

        public Entity AddKnocked(Assets.TilePos newFromDirection, bool newImmediate) {
            var componentPool = GetComponentPool(GameComponentIds.Knocked);
            var component = (Assets.KnockedComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.KnockedComponent());
            component.FromDirection = newFromDirection;
            component.Immediate = newImmediate;
            return AddComponent(GameComponentIds.Knocked, component);
        }

        public Entity ReplaceKnocked(Assets.TilePos newFromDirection, bool newImmediate) {
            var componentPool = GetComponentPool(GameComponentIds.Knocked);
            var component = (Assets.KnockedComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.KnockedComponent());
            component.FromDirection = newFromDirection;
            component.Immediate = newImmediate;
            ReplaceComponent(GameComponentIds.Knocked, component);
            return this;
        }

        public Entity RemoveKnocked() {
            return RemoveComponent(GameComponentIds.Knocked);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherKnocked;

        public static IMatcher Knocked {
            get {
                if (_matcherKnocked == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Knocked);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherKnocked = matcher;
                }

                return _matcherKnocked;
            }
        }
    }
