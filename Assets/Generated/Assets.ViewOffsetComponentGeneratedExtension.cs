using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ViewOffsetComponent viewOffset { get { return (Assets.ViewOffsetComponent)GetComponent(GameComponentIds.ViewOffset); } }

        public bool hasViewOffset { get { return HasComponent(GameComponentIds.ViewOffset); } }

        public Entity AddViewOffset(UnityEngine.Vector3 newValue) {
            var componentPool = GetComponentPool(GameComponentIds.ViewOffset);
            var component = (Assets.ViewOffsetComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ViewOffsetComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.ViewOffset, component);
        }

        public Entity ReplaceViewOffset(UnityEngine.Vector3 newValue) {
            var componentPool = GetComponentPool(GameComponentIds.ViewOffset);
            var component = (Assets.ViewOffsetComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ViewOffsetComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.ViewOffset, component);
            return this;
        }

        public Entity RemoveViewOffset() {
            return RemoveComponent(GameComponentIds.ViewOffset);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherViewOffset;

        public static IMatcher ViewOffset {
            get {
                if (_matcherViewOffset == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ViewOffset);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherViewOffset = matcher;
                }

                return _matcherViewOffset;
            }
        }
    }
