namespace Entitas {
    public partial class Pool {
        public ISystem CreateCursorClickMenuItemSystem() {
            return this.CreateSystem<Assets.MainMenu.CursorClickMenuItemSystem>();
        }
    }
}