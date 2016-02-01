namespace Entitas {
    public partial class Pool {
        public ISystem CreateMoveAnimationSystem() {
            return this.CreateSystem<Assets.Render.MoveAnimationSystem>();
        }
    }
}