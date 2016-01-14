namespace Entitas {
    public partial class Pool {
        public ISystem CreateCurseSwitchSystem() {
            return this.CreateSystem<Assets.CurseSwitchSystem>();
        }
    }
}