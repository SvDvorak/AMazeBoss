using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.RotationComponent rotation { get { return (Assets.RotationComponent)GetComponent(GameComponentIds.Rotation); } }

        public bool hasRotation { get { return HasComponent(GameComponentIds.Rotation); } }

        public Entity AddRotation(int newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Rotation);
            var component = (Assets.RotationComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.RotationComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Rotation, component);
        }

        public Entity ReplaceRotation(int newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Rotation);
            var component = (Assets.RotationComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.RotationComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Rotation, component);
            return this;
        }

        public Entity RemoveRotation() {
            return RemoveComponent(GameComponentIds.Rotation);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherRotation;

        public static IMatcher Rotation {
            get {
                if (_matcherRotation == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Rotation);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherRotation = matcher;
                }

                return _matcherRotation;
            }
        }
    }
