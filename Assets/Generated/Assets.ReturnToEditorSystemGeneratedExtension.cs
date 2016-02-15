namespace Entitas {
    public partial class Pool {
        public ISystem CreateReturnToPreviousViewSystem() {
            return this.CreateSystem<Assets.ReturnToPreviousViewSystem>();
        }
    }
}