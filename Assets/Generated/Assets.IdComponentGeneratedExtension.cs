using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.IdComponent id { get { return (Assets.IdComponent)GetComponent(GameComponentIds.Id); } }

        public bool hasId { get { return HasComponent(GameComponentIds.Id); } }

        public Entity AddId(int newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Id);
            var component = (Assets.IdComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.IdComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Id, component);
        }

        public Entity ReplaceId(int newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Id);
            var component = (Assets.IdComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.IdComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Id, component);
            return this;
        }

        public Entity RemoveId() {
            return RemoveComponent(GameComponentIds.Id);;
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

    public partial class UiMatcher {
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
