using Assets;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateBottomSpawnerSystem() {
            return this.CreateSystem<BottomSpawnerSystem>();
        }
    }
}