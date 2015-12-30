using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.AnimatorComponent animator { get { return (Assets.AnimatorComponent)GetComponent(ComponentIds.Animator); } }

        public bool hasAnimator { get { return HasComponent(ComponentIds.Animator); } }

        static readonly Stack<Assets.AnimatorComponent> _animatorComponentPool = new Stack<Assets.AnimatorComponent>();

        public static void ClearAnimatorComponentPool() {
            _animatorComponentPool.Clear();
        }

        public Entity AddAnimator(UnityEngine.Animator newValue) {
            var component = _animatorComponentPool.Count > 0 ? _animatorComponentPool.Pop() : new Assets.AnimatorComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Animator, component);
        }

        public Entity ReplaceAnimator(UnityEngine.Animator newValue) {
            var previousComponent = hasAnimator ? animator : null;
            var component = _animatorComponentPool.Count > 0 ? _animatorComponentPool.Pop() : new Assets.AnimatorComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Animator, component);
            if (previousComponent != null) {
                _animatorComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAnimator() {
            var component = animator;
            RemoveComponent(ComponentIds.Animator);
            _animatorComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherAnimator;

        public static IMatcher Animator {
            get {
                if (_matcherAnimator == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Animator);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherAnimator = matcher;
                }

                return _matcherAnimator;
            }
        }
    }
}
