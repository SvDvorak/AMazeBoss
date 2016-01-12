namespace Entitas {
    public partial class Pool {
        public ISystem CreatePositionAnimationSystem() {
            return this.CreateSystem<Assets.Render.PositionAnimationSystem>();
        }
    }
}