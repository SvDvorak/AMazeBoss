using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.AnimatorComponent animator { get { return (Assets.AnimatorComponent)GetComponent(GameComponentIds.Animator); } }

        public bool hasAnimator { get { return HasComponent(GameComponentIds.Animator); } }

        static readonly Stack<Assets.AnimatorComponent> _animatorComponentPool = new Stack<Assets.AnimatorComponent>();

        public static void ClearAnimatorComponentPool() {
            _animatorComponentPool.Clear();
        }

        public Entity AddAnimator(UnityEngine.Animator newValue) {
            var component = _animatorComponentPool.Count > 0 ? _animatorComponentPool.Pop() : new Assets.AnimatorComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Animator, component);
        }

        public Entity ReplaceAnimator(UnityEngine.Animator newValue) {
            var previousComponent = hasAnimator ? animator : null;
            var component = _animatorComponentPool.Count > 0 ? _animatorComponentPool.Pop() : new Assets.AnimatorComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Animator, component);
            if (previousComponent != null) {
                _animatorComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAnimator() {
            var component = animator;
            RemoveComponent(GameComponentIds.Animator);
            _animatorComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherAnimator;

        public static IMatcher Animator {
            get {
                if (_matcherAnimator == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Animator);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherAnimator = matcher;
                }

                return _matcherAnimator;
            }
        }
    }
