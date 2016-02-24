using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.AttackingComponent attackingComponent = new Assets.AttackingComponent();

        public bool isAttacking {
            get { return HasComponent(GameComponentIds.Attacking); }
            set {
                if (value != isAttacking) {
                    if (value) {
                        AddComponent(GameComponentIds.Attacking, attackingComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Attacking);
                    }
                }
            }
        }

        public Entity IsAttacking(bool value) {
            isAttacking = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherAttacking;

        public static IMatcher Attacking {
            get {
                if (_matcherAttacking == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Attacking);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherAttacking = matcher;
                }

                return _matcherAttacking;
            }
        }
    }
