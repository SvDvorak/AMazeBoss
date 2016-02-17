using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.HealthComponent health { get { return (Assets.HealthComponent)GetComponent(GameComponentIds.Health); } }

        public bool hasHealth { get { return HasComponent(GameComponentIds.Health); } }

        public Entity AddHealth(int newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Health);
            var component = (Assets.HealthComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.HealthComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Health, component);
        }

        public Entity ReplaceHealth(int newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Health);
            var component = (Assets.HealthComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.HealthComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Health, component);
            return this;
        }

        public Entity RemoveHealth() {
            return RemoveComponent(GameComponentIds.Health);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherHealth;

        public static IMatcher Health {
            get {
                if (_matcherHealth == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Health);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherHealth = matcher;
                }

                return _matcherHealth;
            }
        }
    }
