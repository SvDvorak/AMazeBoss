using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.GameObjectComponent gameObject { get { return (Assets.GameObjectComponent)GetComponent(GameComponentIds.GameObject); } }

        public bool hasGameObject { get { return HasComponent(GameComponentIds.GameObject); } }

        public Entity AddGameObject(Assets.ObjectType newType) {
            var componentPool = GetComponentPool(GameComponentIds.GameObject);
            var component = (Assets.GameObjectComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.GameObjectComponent());
            component.Type = newType;
            return AddComponent(GameComponentIds.GameObject, component);
        }

        public Entity ReplaceGameObject(Assets.ObjectType newType) {
            var componentPool = GetComponentPool(GameComponentIds.GameObject);
            var component = (Assets.GameObjectComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.GameObjectComponent());
            component.Type = newType;
            ReplaceComponent(GameComponentIds.GameObject, component);
            return this;
        }

        public Entity RemoveGameObject() {
            return RemoveComponent(GameComponentIds.GameObject);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherGameObject;

        public static IMatcher GameObject {
            get {
                if (_matcherGameObject == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.GameObject);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherGameObject = matcher;
                }

                return _matcherGameObject;
            }
        }
    }
