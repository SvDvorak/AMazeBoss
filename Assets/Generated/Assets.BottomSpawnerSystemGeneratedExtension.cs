namespace Entitas {
    public partial class Pool {
        public ISystem CreateBottomSpawnerSystem() {
            return this.CreateSystem<Assets.BottomSpawnerSystem>();
        }
    }
}