namespace Entitas {
    public partial class Pool {
        public ISystem CreatePutDownPlaceableSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Input.PutDownPlaceableSystem>();
        }
    }
}