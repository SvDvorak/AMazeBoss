namespace Entitas {
    public partial class Pool {
        public ISystem CreateDeathAnimationSystem() {
            return this.CreateSystem<Assets.Render.DeathAnimationSystem>();
        }
    }
}