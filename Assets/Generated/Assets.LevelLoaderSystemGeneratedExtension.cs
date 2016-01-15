namespace Entitas {
    public partial class Pool {
        public ISystem CreateLevelLoaderSystem() {
            return this.CreateSystem<Assets.LevelLoaderSystem>();
        }
    }
}