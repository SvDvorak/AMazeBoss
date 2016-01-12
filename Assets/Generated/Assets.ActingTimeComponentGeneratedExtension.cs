using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.ActingTimeComponent actingTime { get { return (Assets.ActingTimeComponent)GetComponent(ComponentIds.ActingTime); } }

        public bool hasActingTime { get { return HasComponent(ComponentIds.ActingTime); } }

        static readonly Stack<Assets.ActingTimeComponent> _actingTimeComponentPool = new Stack<Assets.ActingTimeComponent>();

        public static void ClearActingTimeComponentPool() {
            _actingTimeComponentPool.Clear();
        }

        public Entity AddActingTime(float newTimeLeft, System.Action newOnFinished) {
            var component = _actingTimeComponentPool.Count > 0 ? _actingTimeComponentPool.Pop() : new Assets.ActingTimeComponent();
            component.TimeLeft = newTimeLeft;
            component.OnFinished = newOnFinished;
            return AddComponent(ComponentIds.ActingTime, component);
        }

        public Entity ReplaceActingTime(float newTimeLeft, System.Action newOnFinished) {
            var previousComponent = hasActingTime ? actingTime : null;
            var component = _actingTimeComponentPool.Count > 0 ? _actingTimeComponentPool.Pop() : new Assets.ActingTimeComponent();
            component.TimeLeft = newTimeLeft;
            component.OnFinished = newOnFinished;
            ReplaceComponent(ComponentIds.ActingTime, component);
            if (previousComponent != null) {
                _actingTimeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveActingTime() {
            var component = actingTime;
            RemoveComponent(ComponentIds.ActingTime);
            _actingTimeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherActingTime;

        public static IMatcher ActingTime {
            get {
                if (_matcherActingTime == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ActingTime);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherActingTime = matcher;
                }

                return _matcherActingTime;
            }
        }
    }
}
