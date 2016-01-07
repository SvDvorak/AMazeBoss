namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveActingOnDoneSystem() {
            return this.CreateSystem<Assets.RemoveActingOnDoneSystem>();
        }
    }
}