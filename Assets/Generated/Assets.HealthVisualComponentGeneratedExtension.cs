using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.HealthVisualComponent healthVisual { get { return (Assets.HealthVisualComponent)GetComponent(GameComponentIds.HealthVisual); } }

        public bool hasHealthVisual { get { return HasComponent(GameComponentIds.HealthVisual); } }

        static readonly Stack<Assets.HealthVisualComponent> _healthVisualComponentPool = new Stack<Assets.HealthVisualComponent>();

        public static void ClearHealthVisualComponentPool() {
            _healthVisualComponentPool.Clear();
        }

        public Entity AddHealthVisual(UnityEngine.TextMesh newText) {
            var component = _healthVisualComponentPool.Count > 0 ? _healthVisualComponentPool.Pop() : new Assets.HealthVisualComponent();
            component.Text = newText;
            return AddComponent(GameComponentIds.HealthVisual, component);
        }

        public Entity ReplaceHealthVisual(UnityEngine.TextMesh newText) {
            var previousComponent = hasHealthVisual ? healthVisual : null;
            var component = _healthVisualComponentPool.Count > 0 ? _healthVisualComponentPool.Pop() : new Assets.HealthVisualComponent();
            component.Text = newText;
            ReplaceComponent(GameComponentIds.HealthVisual, component);
            if (previousComponent != null) {
                _healthVisualComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveHealthVisual() {
            var component = healthVisual;
            RemoveComponent(GameComponentIds.HealthVisual);
            _healthVisualComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherHealthVisual;

        public static IMatcher HealthVisual {
            get {
                if (_matcherHealthVisual == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.HealthVisual);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherHealthVisual = matcher;
                }

                return _matcherHealthVisual;
            }
        }
    }
