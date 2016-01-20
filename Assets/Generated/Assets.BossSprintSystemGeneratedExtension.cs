namespace Entitas {
    public partial class Pool {
        public ISystem CreateBossSprintSystem() {
            return this.CreateSystem<Assets.BossSprintSystem>();
        }
    }
}