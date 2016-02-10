using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.HealthComponent health { get { return (Assets.HealthComponent)GetComponent(GameComponentIds.Health); } }

        public bool hasHealth { get { return HasComponent(GameComponentIds.Health); } }

        static readonly Stack<Assets.HealthComponent> _healthComponentPool = new Stack<Assets.HealthComponent>();

        public static void ClearHealthComponentPool() {
            _healthComponentPool.Clear();
        }

        public Entity AddHealth(int newValue) {
            var component = _healthComponentPool.Count > 0 ? _healthComponentPool.Pop() : new Assets.HealthComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Health, component);
        }

        public Entity ReplaceHealth(int newValue) {
            var previousComponent = hasHealth ? health : null;
            var component = _healthComponentPool.Count > 0 ? _healthComponentPool.Pop() : new Assets.HealthComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Health, component);
            if (previousComponent != null) {
                _healthComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveHealth() {
            var component = health;
            RemoveComponent(GameComponentIds.Health);
            _healthComponentPool.Push(component);
            return this;
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
