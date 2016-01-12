namespace Entitas {
    public partial class Pool {
        public ISystem CreateTrapLoadedAnimationSystem() {
            return this.CreateSystem<Assets.Render.TrapLoadedAnimationSystem>();
        }
    }
}