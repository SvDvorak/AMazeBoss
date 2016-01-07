namespace Entitas {
    public partial class Pool {
        public ISystem CreateSubtypeSelectorSystem() {
            return this.CreateSystem<Assets.SubtypeSelectorSystem>();
        }
    }
}