namespace Entitas {
    public partial class Pool {
        public ISystem CreateAnimationSystem() {
            return this.CreateSystem<Assets.Render.AnimationSystem>();
        }
    }
}