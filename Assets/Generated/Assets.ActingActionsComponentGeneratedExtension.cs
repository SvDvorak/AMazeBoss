using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ActingActionsComponent actingActions { get { return (Assets.ActingActionsComponent)GetComponent(GameComponentIds.ActingActions); } }

        public bool hasActingActions { get { return HasComponent(GameComponentIds.ActingActions); } }

        public Entity AddActingActions(System.Collections.Generic.Queue<Assets.ActingAction> newActions) {
            var componentPool = GetComponentPool(GameComponentIds.ActingActions);
            var component = (Assets.ActingActionsComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ActingActionsComponent());
            component.Actions = newActions;
            return AddComponent(GameComponentIds.ActingActions, component);
        }

        public Entity ReplaceActingActions(System.Collections.Generic.Queue<Assets.ActingAction> newActions) {
            var componentPool = GetComponentPool(GameComponentIds.ActingActions);
            var component = (Assets.ActingActionsComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ActingActionsComponent());
            component.Actions = newActions;
            ReplaceComponent(GameComponentIds.ActingActions, component);
            return this;
        }

        public Entity RemoveActingActions() {
            return RemoveComponent(GameComponentIds.ActingActions);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherActingActions;

        public static IMatcher ActingActions {
            get {
                if (_matcherActingActions == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ActingActions);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherActingActions = matcher;
                }

                return _matcherActingActions;
            }
        }
    }
