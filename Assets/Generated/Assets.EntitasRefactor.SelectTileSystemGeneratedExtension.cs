using Assets.EntitasRefactor.Input;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectTileSystem() {
            return this.CreateSystem<SelectTileSystem>();
        }
    }
}