namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectPlaceableSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Input.SelectPlaceableSystem>();
        }
    }
}