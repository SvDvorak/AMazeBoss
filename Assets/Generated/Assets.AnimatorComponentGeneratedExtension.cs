using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.AnimatorComponent animator { get { return (Assets.AnimatorComponent)GetComponent(GameComponentIds.Animator); } }

        public bool hasAnimator { get { return HasComponent(GameComponentIds.Animator); } }

        public Entity AddAnimator(UnityEngine.Animator newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Animator);
            var component = (Assets.AnimatorComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.AnimatorComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Animator, component);
        }

        public Entity ReplaceAnimator(UnityEngine.Animator newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Animator);
            var component = (Assets.AnimatorComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.AnimatorComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Animator, component);
            return this;
        }

        public Entity RemoveAnimator() {
            return RemoveComponent(GameComponentIds.Animator);;
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
