namespace Entitas {
    public partial class Pool {
        public ISystem CreateTemplateSelectorSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.TemplateSelectorSystem>();
        }
    }
}