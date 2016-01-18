namespace Entitas {
    public partial class Pool {
        public ISystem CreateCurseSwitchActivatedAnimationSystem() {
            return this.CreateSystem<Assets.Render.CurseSwitchActivatedAnimationSystem>();
        }
    }
}