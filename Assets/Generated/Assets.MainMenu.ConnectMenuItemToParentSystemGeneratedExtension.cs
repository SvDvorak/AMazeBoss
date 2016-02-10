namespace Entitas {
    public partial class Pool {
        public ISystem CreateConnectMenuItemToParentSystem() {
            return this.CreateSystem<Assets.MainMenu.ConnectMenuItemToParentSystem>();
        }
    }
}