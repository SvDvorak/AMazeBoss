namespace Entitas {
    public partial class Pool {
        public ISystem CreateRenderPositionsSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Render.RenderPositionsSystem>();
        }
    }
}