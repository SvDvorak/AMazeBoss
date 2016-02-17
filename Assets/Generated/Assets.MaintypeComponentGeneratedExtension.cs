using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MaintypeComponent maintype { get { return (Assets.MaintypeComponent)GetComponent(GameComponentIds.Maintype); } }

        public bool hasMaintype { get { return HasComponent(GameComponentIds.Maintype); } }

        public Entity AddMaintype(string newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Maintype);
            var component = (Assets.MaintypeComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MaintypeComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Maintype, component);
        }

        public Entity ReplaceMaintype(string newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Maintype);
            var component = (Assets.MaintypeComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MaintypeComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Maintype, component);
            return this;
        }

        public Entity RemoveMaintype() {
            return RemoveComponent(GameComponentIds.Maintype);;
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
