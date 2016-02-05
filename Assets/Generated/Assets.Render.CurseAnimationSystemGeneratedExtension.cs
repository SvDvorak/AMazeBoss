namespace Entitas {
    public partial class Pool {
        public ISystem CreateCurseAnimationSystem() {
            return this.CreateSystem<Assets.Render.CurseAnimationSystem>();
        }
    }
}