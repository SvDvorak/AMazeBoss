namespace Entitas {
    public partial class Pool {
        public ISystem CreateKnockBoxSystem() {
            return this.CreateSystem<Assets.KnockBoxSystem>();
        }
    }
}