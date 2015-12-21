namespace Entitas {
    public partial class Pool {
        public ISystem CreateWallAdjustmentSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.WallAdjustmentSystem>();
        }
    }
}