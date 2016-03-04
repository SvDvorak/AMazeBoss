using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ExitGateComponent exitGate { get { return (Assets.ExitGateComponent)GetComponent(GameComponentIds.ExitGate); } }

        public bool hasExitGate { get { return HasComponent(GameComponentIds.ExitGate); } }

        public Entity AddExitGate(bool newUnlocked) {
            var componentPool = GetComponentPool(GameComponentIds.ExitGate);
            var component = (Assets.ExitGateComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ExitGateComponent());
            component.Locked = newUnlocked;
            return AddComponent(GameComponentIds.ExitGate, component);
        }

        public Entity ReplaceExitGate(bool newUnlocked) {
            var componentPool = GetComponentPool(GameComponentIds.ExitGate);
            var component = (Assets.ExitGateComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ExitGateComponent());
            component.Locked = newUnlocked;
            ReplaceComponent(GameComponentIds.ExitGate, component);
            return this;
        }

        public Entity RemoveExitGate() {
            return RemoveComponent(GameComponentIds.ExitGate);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherExitGate;

        public static IMatcher ExitGate {
            get {
                if (_matcherExitGate == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ExitGate);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherExitGate = matcher;
                }

                return _matcherExitGate;
            }
        }
    }
