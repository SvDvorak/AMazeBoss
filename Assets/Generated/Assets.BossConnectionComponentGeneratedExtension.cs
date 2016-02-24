using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.BossConnectionComponent bossConnection { get { return (Assets.BossConnectionComponent)GetComponent(GameComponentIds.BossConnection); } }

        public bool hasBossConnection { get { return HasComponent(GameComponentIds.BossConnection); } }

        public Entity AddBossConnection(int newId) {
            var componentPool = GetComponentPool(GameComponentIds.BossConnection);
            var component = (Assets.BossConnectionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.BossConnectionComponent());
            component.Id = newId;
            return AddComponent(GameComponentIds.BossConnection, component);
        }

        public Entity ReplaceBossConnection(int newId) {
            var componentPool = GetComponentPool(GameComponentIds.BossConnection);
            var component = (Assets.BossConnectionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.BossConnectionComponent());
            component.Id = newId;
            ReplaceComponent(GameComponentIds.BossConnection, component);
            return this;
        }

        public Entity RemoveBossConnection() {
            return RemoveComponent(GameComponentIds.BossConnection);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherBossConnection;

        public static IMatcher BossConnection {
            get {
                if (_matcherBossConnection == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.BossConnection);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherBossConnection = matcher;
                }

                return _matcherBossConnection;
            }
        }
    }
