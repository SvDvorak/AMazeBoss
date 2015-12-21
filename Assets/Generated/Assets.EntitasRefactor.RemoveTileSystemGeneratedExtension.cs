using Assets.EntitasRefactor.Input;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveTileSystem() {
            return this.CreateSystem<RemoveTileSystem>();
        }
    }
}