namespace Entitas {
    public partial class Pool {
        public ISystem CreateTrapActivatedAnimationSystem() {
            return this.CreateSystem<Assets.Render.TrapActivatedAnimationSystem>();
        }
    }
}