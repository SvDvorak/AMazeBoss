namespace Entitas {
    public partial class Pool {
        public ISystem CreateBossMoveSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.BossMoveSystem>();
        }
    }
}