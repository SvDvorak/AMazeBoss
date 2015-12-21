using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.RotationComponent rotation { get { return (Assets.EntitasRefactor.RotationComponent)GetComponent(ComponentIds.Rotation); } }

        public bool hasRotation { get { return HasComponent(ComponentIds.Rotation); } }

        static readonly Stack<Assets.EntitasRefactor.RotationComponent> _rotationComponentPool = new Stack<Assets.EntitasRefactor.RotationComponent>();

        public static void ClearRotationComponentPool() {
            _rotationComponentPool.Clear();
        }

        public Entity AddRotation(int newValue) {
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new Assets.EntitasRefactor.RotationComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Rotation, component);
        }

        public Entity ReplaceRotation(int newValue) {
            var previousComponent = hasRotation ? rotation : null;
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new Assets.EntitasRefactor.RotationComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Rotation, component);
            if (previousComponent != null) {
                _rotationComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRotation() {
            var component = rotation;
            RemoveComponent(ComponentIds.Rotation);
            _rotationComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherRotation;

        public static IMatcher Rotation {
            get {
                if (_matcherRotation == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Rotation);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherRotation = matcher;
                }

                return _matcherRotation;
            }
        }
    }
}
