using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ActingTimeComponent actingTime { get { return (Assets.ActingTimeComponent)GetComponent(GameComponentIds.ActingTime); } }

        public bool hasActingTime { get { return HasComponent(GameComponentIds.ActingTime); } }

        static readonly Stack<Assets.ActingTimeComponent> _actingTimeComponentPool = new Stack<Assets.ActingTimeComponent>();

        public static void ClearActingTimeComponentPool() {
            _actingTimeComponentPool.Clear();
        }

        public Entity AddActingTime(float newTimeLeft) {
            var component = _actingTimeComponentPool.Count > 0 ? _actingTimeComponentPool.Pop() : new Assets.ActingTimeComponent();
            component.TimeLeft = newTimeLeft;
            return AddComponent(GameComponentIds.ActingTime, component);
        }

        public Entity ReplaceActingTime(float newTimeLeft) {
            var previousComponent = hasActingTime ? actingTime : null;
            var component = _actingTimeComponentPool.Count > 0 ? _actingTimeComponentPool.Pop() : new Assets.ActingTimeComponent();
            component.TimeLeft = newTimeLeft;
            ReplaceComponent(GameComponentIds.ActingTime, component);
            if (previousComponent != null) {
                _actingTimeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveActingTime() {
            var component = actingTime;
            RemoveComponent(GameComponentIds.ActingTime);
            _actingTimeComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherActingTime;

        public static IMatcher ActingTime {
            get {
                if (_matcherActingTime == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ActingTime);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherActingTime = matcher;
                }

                return _matcherActingTime;
            }
        }
    }
