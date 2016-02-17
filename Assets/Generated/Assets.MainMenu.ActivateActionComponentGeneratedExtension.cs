using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.ActivateActionComponent activateAction { get { return (Assets.MainMenu.ActivateActionComponent)GetComponent(UiComponentIds.ActivateAction); } }

        public bool hasActivateAction { get { return HasComponent(UiComponentIds.ActivateAction); } }

        public Entity AddActivateAction(System.Action newAction) {
            var componentPool = GetComponentPool(UiComponentIds.ActivateAction);
            var component = (Assets.MainMenu.ActivateActionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MainMenu.ActivateActionComponent());
            component.Action = newAction;
            return AddComponent(UiComponentIds.ActivateAction, component);
        }

        public Entity ReplaceActivateAction(System.Action newAction) {
            var componentPool = GetComponentPool(UiComponentIds.ActivateAction);
            var component = (Assets.MainMenu.ActivateActionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MainMenu.ActivateActionComponent());
            component.Action = newAction;
            ReplaceComponent(UiComponentIds.ActivateAction, component);
            return this;
        }

        public Entity RemoveActivateAction() {
            return RemoveComponent(UiComponentIds.ActivateAction);;
        }
    }
}

    public partial class UiMatcher {
        static IMatcher _matcherActivateAction;

        public static IMatcher ActivateAction {
            get {
                if (_matcherActivateAction == null) {
                    var matcher = (Matcher)Matcher.AllOf(UiComponentIds.ActivateAction);
                    matcher.componentNames = UiComponentIds.componentNames;
                    _matcherActivateAction = matcher;
                }

                return _matcherActivateAction;
            }
        }
    }
