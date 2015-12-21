using Assets.EntitasRefactor.Render;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateRenderPositionsSystem() {
            return this.CreateSystem<RenderPositionsSystem>();
        }
    }
}