using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.HealthVisualComponent healthVisual { get { return (Assets.HealthVisualComponent)GetComponent(GameComponentIds.HealthVisual); } }

        public bool hasHealthVisual { get { return HasComponent(GameComponentIds.HealthVisual); } }

        public Entity AddHealthVisual(UnityEngine.TextMesh newText) {
            var componentPool = GetComponentPool(GameComponentIds.HealthVisual);
            var component = (Assets.HealthVisualComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.HealthVisualComponent());
            component.Text = newText;
            return AddComponent(GameComponentIds.HealthVisual, component);
        }

        public Entity ReplaceHealthVisual(UnityEngine.TextMesh newText) {
            var componentPool = GetComponentPool(GameComponentIds.HealthVisual);
            var component = (Assets.HealthVisualComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.HealthVisualComponent());
            component.Text = newText;
            ReplaceComponent(GameComponentIds.HealthVisual, component);
            return this;
        }

        public Entity RemoveHealthVisual() {
            return RemoveComponent(GameComponentIds.HealthVisual);;
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
