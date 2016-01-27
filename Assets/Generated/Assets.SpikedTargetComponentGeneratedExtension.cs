using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.SpikedTargetComponent spikedTarget { get { return (Assets.SpikedTargetComponent)GetComponent(ComponentIds.SpikedTarget); } }

        public bool hasSpikedTarget { get { return HasComponent(ComponentIds.SpikedTarget); } }

        static readonly Stack<Assets.SpikedTargetComponent> _spikedTargetComponentPool = new Stack<Assets.SpikedTargetComponent>();

        public static void ClearSpikedTargetComponentPool() {
            _spikedTargetComponentPool.Clear();
        }

        public Entity AddSpikedTarget(int newTargetId) {
            var component = _spikedTargetComponentPool.Count > 0 ? _spikedTargetComponentPool.Pop() : new Assets.SpikedTargetComponent();
            component.TargetId = newTargetId;
            return AddComponent(ComponentIds.SpikedTarget, component);
        }

        public Entity ReplaceSpikedTarget(int newTargetId) {
            var previousComponent = hasSpikedTarget ? spikedTarget : null;
            var component = _spikedTargetComponentPool.Count > 0 ? _spikedTargetComponentPool.Pop() : new Assets.SpikedTargetComponent();
            component.TargetId = newTargetId;
            ReplaceComponent(ComponentIds.SpikedTarget, component);
            if (previousComponent != null) {
                _spikedTargetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSpikedTarget() {
            var component = spikedTarget;
            RemoveComponent(ComponentIds.SpikedTarget);
            _spikedTargetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpikedTarget;

        public static IMatcher SpikedTarget {
            get {
                if (_matcherSpikedTarget == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SpikedTarget);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSpikedTarget = matcher;
                }

                return _matcherSpikedTarget;
            }
        }
    }
}
