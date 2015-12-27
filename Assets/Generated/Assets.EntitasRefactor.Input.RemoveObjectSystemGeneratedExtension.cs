namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveObjectSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Input.RemoveObjectSystem>();
        }
    }
}