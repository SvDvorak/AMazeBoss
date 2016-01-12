using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.IdComponent id { get { return (Assets.IdComponent)GetComponent(ComponentIds.Id); } }

        public bool hasId { get { return HasComponent(ComponentIds.Id); } }

        static readonly Stack<Assets.IdComponent> _idComponentPool = new Stack<Assets.IdComponent>();

        public static void ClearIdComponentPool() {
            _idComponentPool.Clear();
        }

        public Entity AddId(int newValue) {
            var component = _idComponentPool.Count > 0 ? _idComponentPool.Pop() : new Assets.IdComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Id, component);
        }

        public Entity ReplaceId(int newValue) {
            var previousComponent = hasId ? id : null;
            var component = _idComponentPool.Count > 0 ? _idComponentPool.Pop() : new Assets.IdComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Id, component);
            if (previousComponent != null) {
                _idComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveId() {
            var component = id;
            RemoveComponent(ComponentIds.Id);
            _idComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherId;

        public static IMatcher Id {
            get {
                if (_matcherId == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Id);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherId = matcher;
                }

                return _matcherId;
            }
        }
    }
}
