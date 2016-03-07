using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.BumpedIntoObjectComponent bumpedIntoObjectComponent = new Assets.BumpedIntoObjectComponent();

        public bool hasBumpedIntoObject {
            get { return HasComponent(GameComponentIds.BumpedIntoObject); }
            set {
                if (value != hasBumpedIntoObject) {
                    if (value) {
                        AddComponent(GameComponentIds.BumpedIntoObject, bumpedIntoObjectComponent);
                    } else {
                        RemoveComponent(GameComponentIds.BumpedIntoObject);
                    }
                }
            }
        }

        public Entity HasBumpedIntoObject(bool value) {
            hasBumpedIntoObject = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherBumpedIntoObject;

        public static IMatcher BumpedIntoObject {
            get {
                if (_matcherBumpedIntoObject == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.BumpedIntoObject);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherBumpedIntoObject = matcher;
                }

                return _matcherBumpedIntoObject;
            }
        }
    }
