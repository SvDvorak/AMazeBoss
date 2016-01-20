namespace Entitas {
    public partial class Pool {
        public ISystem CreateMoveHistorySystem() {
            return this.CreateSystem<Assets.MoveHistorySystem>();
        }
    }
}