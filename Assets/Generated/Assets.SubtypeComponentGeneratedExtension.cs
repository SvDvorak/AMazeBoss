using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.SubtypeComponent subtype { get { return (Assets.SubtypeComponent)GetComponent(GameComponentIds.Subtype); } }

        public bool hasSubtype { get { return HasComponent(GameComponentIds.Subtype); } }

        public Entity AddSubtype(string newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Subtype);
            var component = (Assets.SubtypeComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.SubtypeComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Subtype, component);
        }

        public Entity ReplaceSubtype(string newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Subtype);
            var component = (Assets.SubtypeComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.SubtypeComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Subtype, component);
            return this;
        }

        public Entity RemoveSubtype() {
            return RemoveComponent(GameComponentIds.Subtype);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSubtype;

        public static IMatcher Subtype {
            get {
                if (_matcherSubtype == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Subtype);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSubtype = matcher;
                }

                return _matcherSubtype;
            }
        }
    }
