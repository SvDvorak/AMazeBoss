namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectedItemAnimationSystem() {
            return this.CreateSystem<Assets.MainMenu.SelectedItemAnimationSystem>();
        }
    }
}