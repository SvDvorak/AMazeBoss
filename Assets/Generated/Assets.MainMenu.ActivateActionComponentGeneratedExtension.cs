using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.ActivateActionComponent activateAction { get { return (Assets.MainMenu.ActivateActionComponent)GetComponent(UiComponentIds.ActivateAction); } }

        public bool hasActivateAction { get { return HasComponent(UiComponentIds.ActivateAction); } }

        static readonly Stack<Assets.MainMenu.ActivateActionComponent> _activateActionComponentPool = new Stack<Assets.MainMenu.ActivateActionComponent>();

        public static void ClearActivateActionComponentPool() {
            _activateActionComponentPool.Clear();
        }

        public Entity AddActivateAction(System.Action newAction) {
            var component = _activateActionComponentPool.Count > 0 ? _activateActionComponentPool.Pop() : new Assets.MainMenu.ActivateActionComponent();
            component.Action = newAction;
            return AddComponent(UiComponentIds.ActivateAction, component);
        }

        public Entity ReplaceActivateAction(System.Action newAction) {
            var previousComponent = hasActivateAction ? activateAction : null;
            var component = _activateActionComponentPool.Count > 0 ? _activateActionComponentPool.Pop() : new Assets.MainMenu.ActivateActionComponent();
            component.Action = newAction;
            ReplaceComponent(UiComponentIds.ActivateAction, component);
            if (previousComponent != null) {
                _activateActionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveActivateAction() {
            var component = activateAction;
            RemoveComponent(UiComponentIds.ActivateAction);
            _activateActionComponentPool.Push(component);
            return this;
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
