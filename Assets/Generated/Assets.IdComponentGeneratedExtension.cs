using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.IdComponent id { get { return (Assets.IdComponent)GetComponent(GameComponentIds.Id); } }

        public bool hasId { get { return HasComponent(GameComponentIds.Id); } }

        static readonly Stack<Assets.IdComponent> _idComponentPool = new Stack<Assets.IdComponent>();

        public static void ClearIdComponentPool() {
            _idComponentPool.Clear();
        }

        public Entity AddId(int newValue) {
            var component = _idComponentPool.Count > 0 ? _idComponentPool.Pop() : new Assets.IdComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Id, component);
        }

        public Entity ReplaceId(int newValue) {
            var previousComponent = hasId ? id : null;
            var component = _idComponentPool.Count > 0 ? _idComponentPool.Pop() : new Assets.IdComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Id, component);
            if (previousComponent != null) {
                _idComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveId() {
            var component = id;
            RemoveComponent(GameComponentIds.Id);
            _idComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherId;

        public static IMatcher Id {
            get {
                if (_matcherId == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Id);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherId = matcher;
                }

                return _matcherId;
            }
        }
    }
