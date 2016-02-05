namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlayerRestartSystem() {
            return this.CreateSystem<Assets.PlayerRestartSystem>();
        }
    }
}