using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.RotationComponent rotation { get { return (Assets.RotationComponent)GetComponent(GameComponentIds.Rotation); } }

        public bool hasRotation { get { return HasComponent(GameComponentIds.Rotation); } }

        static readonly Stack<Assets.RotationComponent> _rotationComponentPool = new Stack<Assets.RotationComponent>();

        public static void ClearRotationComponentPool() {
            _rotationComponentPool.Clear();
        }

        public Entity AddRotation(int newValue) {
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new Assets.RotationComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Rotation, component);
        }

        public Entity ReplaceRotation(int newValue) {
            var previousComponent = hasRotation ? rotation : null;
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new Assets.RotationComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Rotation, component);
            if (previousComponent != null) {
                _rotationComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRotation() {
            var component = rotation;
            RemoveComponent(GameComponentIds.Rotation);
            _rotationComponentPool.Push(component);
            return this;
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
