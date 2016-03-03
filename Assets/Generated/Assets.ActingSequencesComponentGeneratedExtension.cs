using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ActingSequencesComponent actingSequences { get { return (Assets.ActingSequencesComponent)GetComponent(GameComponentIds.ActingSequences); } }

        public bool hasActingSequences { get { return HasComponent(GameComponentIds.ActingSequences); } }

        public Entity AddActingSequences(System.Collections.Generic.Queue<Assets.ActingSequence> newSequences) {
            var componentPool = GetComponentPool(GameComponentIds.ActingSequences);
            var component = (Assets.ActingSequencesComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ActingSequencesComponent());
            component.Sequences = newSequences;
            return AddComponent(GameComponentIds.ActingSequences, component);
        }

        public Entity ReplaceActingSequences(System.Collections.Generic.Queue<Assets.ActingSequence> newSequences) {
            var componentPool = GetComponentPool(GameComponentIds.ActingSequences);
            var component = (Assets.ActingSequencesComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ActingSequencesComponent());
            component.Sequences = newSequences;
            ReplaceComponent(GameComponentIds.ActingSequences, component);
            return this;
        }

        public Entity RemoveActingSequences() {
            return RemoveComponent(GameComponentIds.ActingSequences);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherActingSequences;

        public static IMatcher ActingSequences {
            get {
                if (_matcherActingSequences == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ActingSequences);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherActingSequences = matcher;
                }

                return _matcherActingSequences;
            }
        }
    }
