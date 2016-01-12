using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.HealthVisualComponent healthVisual { get { return (Assets.HealthVisualComponent)GetComponent(ComponentIds.HealthVisual); } }

        public bool hasHealthVisual { get { return HasComponent(ComponentIds.HealthVisual); } }

        static readonly Stack<Assets.HealthVisualComponent> _healthVisualComponentPool = new Stack<Assets.HealthVisualComponent>();

        public static void ClearHealthVisualComponentPool() {
            _healthVisualComponentPool.Clear();
        }

        public Entity AddHealthVisual(UnityEngine.TextMesh newText) {
            var component = _healthVisualComponentPool.Count > 0 ? _healthVisualComponentPool.Pop() : new Assets.HealthVisualComponent();
            component.Text = newText;
            return AddComponent(ComponentIds.HealthVisual, component);
        }

        public Entity ReplaceHealthVisual(UnityEngine.TextMesh newText) {
            var previousComponent = hasHealthVisual ? healthVisual : null;
            var component = _healthVisualComponentPool.Count > 0 ? _healthVisualComponentPool.Pop() : new Assets.HealthVisualComponent();
            component.Text = newText;
            ReplaceComponent(ComponentIds.HealthVisual, component);
            if (previousComponent != null) {
                _healthVisualComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveHealthVisual() {
            var component = healthVisual;
            RemoveComponent(ComponentIds.HealthVisual);
            _healthVisualComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherHealthVisual;

        public static IMatcher HealthVisual {
            get {
                if (_matcherHealthVisual == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.HealthVisual);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherHealthVisual = matcher;
                }

                return _matcherHealthVisual;
            }
        }
    }
}
