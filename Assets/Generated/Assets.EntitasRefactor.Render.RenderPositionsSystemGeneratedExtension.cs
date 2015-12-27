using Assets.Render;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateRenderPositionsSystem() {
            return this.CreateSystem<RenderPositionsSystem>();
        }
    }
}