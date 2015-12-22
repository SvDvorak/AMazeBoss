namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveTileSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Input.RemoveObjectSystem>();
        }
    }
}