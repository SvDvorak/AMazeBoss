namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddViewSystem() {
            return this.CreateSystem<Assets.Render.AddViewSystem>();
        }
    }
}