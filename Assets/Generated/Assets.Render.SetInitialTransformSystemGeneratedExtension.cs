namespace Entitas {
    public partial class Pool {
        public ISystem CreateSetInitialTransformSystem() {
            return this.CreateSystem<Assets.Render.SetInitialTransformSystem>();
        }
    }
}