using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.MaintypeComponent maintype { get { return (Assets.EntitasRefactor.MaintypeComponent)GetComponent(ComponentIds.Maintype); } }

        public bool hasMaintype { get { return HasComponent(ComponentIds.Maintype); } }

        static readonly Stack<Assets.EntitasRefactor.MaintypeComponent> _maintypeComponentPool = new Stack<Assets.EntitasRefactor.MaintypeComponent>();

        public static void ClearMaintypeComponentPool() {
            _maintypeComponentPool.Clear();
        }

        public Entity AddMaintype(string newValue) {
            var component = _maintypeComponentPool.Count > 0 ? _maintypeComponentPool.Pop() : new Assets.EntitasRefactor.MaintypeComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Maintype, component);
        }

        public Entity ReplaceMaintype(string newValue) {
            var previousComponent = hasMaintype ? maintype : null;
            var component = _maintypeComponentPool.Count > 0 ? _maintypeComponentPool.Pop() : new Assets.EntitasRefactor.MaintypeComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Maintype, component);
            if (previousComponent != null) {
                _maintypeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMaintype() {
            var component = maintype;
            RemoveComponent(ComponentIds.Maintype);
            _maintypeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMaintype;

        public static IMatcher Maintype {
            get {
                if (_matcherMaintype == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Maintype);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMaintype = matcher;
                }

                return _matcherMaintype;
            }
        }
    }
}
