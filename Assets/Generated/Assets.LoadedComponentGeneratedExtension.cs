using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LoadedComponent loaded { get { return (Assets.LoadedComponent)GetComponent(GameComponentIds.Loaded); } }

        public bool hasLoaded { get { return HasComponent(GameComponentIds.Loaded); } }

        public Entity AddLoaded(bool newLoadedThisTurn) {
            var componentPool = GetComponentPool(GameComponentIds.Loaded);
            var component = (Assets.LoadedComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LoadedComponent());
            component.LoadedThisTurn = newLoadedThisTurn;
            return AddComponent(GameComponentIds.Loaded, component);
        }

        public Entity ReplaceLoaded(bool newLoadedThisTurn) {
            var componentPool = GetComponentPool(GameComponentIds.Loaded);
            var component = (Assets.LoadedComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LoadedComponent());
            component.LoadedThisTurn = newLoadedThisTurn;
            ReplaceComponent(GameComponentIds.Loaded, component);
            return this;
        }

        public Entity RemoveLoaded() {
            return RemoveComponent(GameComponentIds.Loaded);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherLoaded;

        public static IMatcher Loaded {
            get {
                if (_matcherLoaded == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Loaded);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherLoaded = matcher;
                }

                return _matcherLoaded;
            }
        }
    }
