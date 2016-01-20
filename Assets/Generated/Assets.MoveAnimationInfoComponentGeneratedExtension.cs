using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.MoveAnimationInfoComponent moveAnimationInfo { get { return (Assets.MoveAnimationInfoComponent)GetComponent(ComponentIds.MoveAnimationInfo); } }

        public bool hasMoveAnimationInfo { get { return HasComponent(ComponentIds.MoveAnimationInfo); } }

        static readonly Stack<Assets.MoveAnimationInfoComponent> _moveAnimationInfoComponentPool = new Stack<Assets.MoveAnimationInfoComponent>();

        public static void ClearMoveAnimationInfoComponentPool() {
            _moveAnimationInfoComponentPool.Clear();
        }

        public Entity AddMoveAnimationInfo(DG.Tweening.Ease newEase, float newTime) {
            var component = _moveAnimationInfoComponentPool.Count > 0 ? _moveAnimationInfoComponentPool.Pop() : new Assets.MoveAnimationInfoComponent();
            component.Ease = newEase;
            component.Time = newTime;
            return AddComponent(ComponentIds.MoveAnimationInfo, component);
        }

        public Entity ReplaceMoveAnimationInfo(DG.Tweening.Ease newEase, float newTime) {
            var previousComponent = hasMoveAnimationInfo ? moveAnimationInfo : null;
            var component = _moveAnimationInfoComponentPool.Count > 0 ? _moveAnimationInfoComponentPool.Pop() : new Assets.MoveAnimationInfoComponent();
            component.Ease = newEase;
            component.Time = newTime;
            ReplaceComponent(ComponentIds.MoveAnimationInfo, component);
            if (previousComponent != null) {
                _moveAnimationInfoComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMoveAnimationInfo() {
            var component = moveAnimationInfo;
            RemoveComponent(ComponentIds.MoveAnimationInfo);
            _moveAnimationInfoComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMoveAnimationInfo;

        public static IMatcher MoveAnimationInfo {
            get {
                if (_matcherMoveAnimationInfo == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MoveAnimationInfo);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMoveAnimationInfo = matcher;
                }

                return _matcherMoveAnimationInfo;
            }
        }
    }
}
