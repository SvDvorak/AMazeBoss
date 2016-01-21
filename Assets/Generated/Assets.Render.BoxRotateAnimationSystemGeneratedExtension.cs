namespace Entitas {
    public partial class Pool {
        public ISystem CreateBoxRotateAnimationSystem() {
            return this.CreateSystem<Assets.Render.BoxRotateAnimationSystem>();
        }
    }
}