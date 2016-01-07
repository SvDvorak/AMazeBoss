namespace Entitas {
    public partial class Pool {
        public ISystem CreateBossMoveSystem() {
            return this.CreateSystem<Assets.BossMoveSystem>();
        }
    }
}