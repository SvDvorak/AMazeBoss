namespace Entitas {
    public partial class Pool {
        public ISystem CreateHealthChangedAnimationSystem() {
            return this.CreateSystem<Assets.Render.HealthChangedAnimationSystem>();
        }
    }
}