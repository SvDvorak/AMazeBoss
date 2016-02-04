namespace Entitas {
    public partial class Pool {
        public ISystem CreateLevelClearedSystem() {
            return this.CreateSystem<Assets.LevelClearedSystem>();
        }
    }
}