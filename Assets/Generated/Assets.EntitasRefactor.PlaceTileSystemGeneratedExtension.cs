using Assets.EntitasRefactor.Input;

namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlaceTileSystem() {
            return this.CreateSystem<PlaceTileSystem>();
        }
    }
}