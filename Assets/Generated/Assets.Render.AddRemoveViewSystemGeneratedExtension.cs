namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddRemoveViewSystem() {
            return this.CreateSystem<Assets.Render.AddRemoveViewSystem>();
        }
    }
}