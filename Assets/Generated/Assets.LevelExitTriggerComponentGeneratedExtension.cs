using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelExitTriggerComponent levelExitTriggerComponent = new Assets.LevelExitTriggerComponent();

        public bool isLevelExitTrigger {
            get { return HasComponent(GameComponentIds.LevelExitTrigger); }
            set {
                if (value != isLevelExitTrigger) {
                    if (value) {
                        AddComponent(GameComponentIds.LevelExitTrigger, levelExitTriggerComponent);
                    } else {
                        RemoveComponent(GameComponentIds.LevelExitTrigger);
                    }
                }
            }
        }

        public Entity IsLevelExitTrigger(bool value) {
            isLevelExitTrigger = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherLevelExitTrigger;

        public static IMatcher LevelExitTrigger {
            get {
                if (_matcherLevelExitTrigger == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.LevelExitTrigger);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherLevelExitTrigger = matcher;
                }

                return _matcherLevelExitTrigger;
            }
        }
    }
