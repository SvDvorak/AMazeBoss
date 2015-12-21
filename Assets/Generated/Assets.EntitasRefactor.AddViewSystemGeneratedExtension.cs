using Assets.EntitasRefactor.Render;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddViewSystem() {
            return this.CreateSystem<AddViewSystem>();
        }
    }
}