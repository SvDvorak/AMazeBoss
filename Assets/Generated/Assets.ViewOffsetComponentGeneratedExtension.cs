using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.ViewOffsetComponent viewOffset { get { return (Assets.ViewOffsetComponent)GetComponent(ComponentIds.ViewOffset); } }

        public bool hasViewOffset { get { return HasComponent(ComponentIds.ViewOffset); } }

        static readonly Stack<Assets.ViewOffsetComponent> _viewOffsetComponentPool = new Stack<Assets.ViewOffsetComponent>();

        public static void ClearViewOffsetComponentPool() {
            _viewOffsetComponentPool.Clear();
        }

        public Entity AddViewOffset(UnityEngine.Vector3 newValue) {
            var component = _viewOffsetComponentPool.Count > 0 ? _viewOffsetComponentPool.Pop() : new Assets.ViewOffsetComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.ViewOffset, component);
        }

        public Entity ReplaceViewOffset(UnityEngine.Vector3 newValue) {
            var previousComponent = hasViewOffset ? viewOffset : null;
            var component = _viewOffsetComponentPool.Count > 0 ? _viewOffsetComponentPool.Pop() : new Assets.ViewOffsetComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.ViewOffset, component);
            if (previousComponent != null) {
                _viewOffsetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveViewOffset() {
            var component = viewOffset;
            RemoveComponent(ComponentIds.ViewOffset);
            _viewOffsetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherViewOffset;

        public static IMatcher ViewOffset {
            get {
                if (_matcherViewOffset == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ViewOffset);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherViewOffset = matcher;
                }

                return _matcherViewOffset;
            }
        }
    }
}
