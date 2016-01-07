namespace Entitas {
    public partial class Pool {
        public ISystem CreateNextTurnSystem() {
            return this.CreateSystem<Assets.NextTurnSystem>();
        }
    }
}