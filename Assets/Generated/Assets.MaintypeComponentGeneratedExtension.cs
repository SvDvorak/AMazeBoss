using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MaintypeComponent maintype { get { return (Assets.MaintypeComponent)GetComponent(GameComponentIds.Maintype); } }

        public bool hasMaintype { get { return HasComponent(GameComponentIds.Maintype); } }

        static readonly Stack<Assets.MaintypeComponent> _maintypeComponentPool = new Stack<Assets.MaintypeComponent>();

        public static void ClearMaintypeComponentPool() {
            _maintypeComponentPool.Clear();
        }

        public Entity AddMaintype(string newValue) {
            var component = _maintypeComponentPool.Count > 0 ? _maintypeComponentPool.Pop() : new Assets.MaintypeComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Maintype, component);
        }

        public Entity ReplaceMaintype(string newValue) {
            var previousComponent = hasMaintype ? maintype : null;
            var component = _maintypeComponentPool.Count > 0 ? _maintypeComponentPool.Pop() : new Assets.MaintypeComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Maintype, component);
            if (previousComponent != null) {
                _maintypeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMaintype() {
            var component = maintype;
            RemoveComponent(GameComponentIds.Maintype);
            _maintypeComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherMaintype;

        public static IMatcher Maintype {
            get {
                if (_matcherMaintype == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Maintype);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherMaintype = matcher;
                }

                return _matcherMaintype;
            }
        }
    }
