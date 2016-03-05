using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.ExitTriggerComponent exitTriggerComponent = new Assets.ExitTriggerComponent();

        public bool isExitTrigger {
            get { return HasComponent(GameComponentIds.ExitTrigger); }
            set {
                if (value != isExitTrigger) {
                    if (value) {
                        AddComponent(GameComponentIds.ExitTrigger, exitTriggerComponent);
                    } else {
                        RemoveComponent(GameComponentIds.ExitTrigger);
                    }
                }
            }
        }

        public Entity IsExitTrigger(bool value) {
            isExitTrigger = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherExitTrigger;

        public static IMatcher ExitTrigger {
            get {
                if (_matcherExitTrigger == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ExitTrigger);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherExitTrigger = matcher;
                }

                return _matcherExitTrigger;
            }
        }
    }
