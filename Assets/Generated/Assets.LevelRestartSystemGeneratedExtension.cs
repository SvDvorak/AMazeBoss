namespace Entitas {
    public partial class Pool {
        public ISystem CreateLevelRestartSystem() {
            return this.CreateSystem<Assets.LevelRestartSystem>();
        }
    }
}