using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ActingTimeComponent actingTime { get { return (Assets.ActingTimeComponent)GetComponent(GameComponentIds.ActingTime); } }

        public bool hasActingTime { get { return HasComponent(GameComponentIds.ActingTime); } }

        public Entity AddActingTime(float newTimeLeft) {
            var componentPool = GetComponentPool(GameComponentIds.ActingTime);
            var component = (Assets.ActingTimeComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ActingTimeComponent());
            component.TimeLeft = newTimeLeft;
            return AddComponent(GameComponentIds.ActingTime, component);
        }

        public Entity ReplaceActingTime(float newTimeLeft) {
            var componentPool = GetComponentPool(GameComponentIds.ActingTime);
            var component = (Assets.ActingTimeComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ActingTimeComponent());
            component.TimeLeft = newTimeLeft;
            ReplaceComponent(GameComponentIds.ActingTime, component);
            return this;
        }

        public Entity RemoveActingTime() {
            return RemoveComponent(GameComponentIds.ActingTime);;
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
