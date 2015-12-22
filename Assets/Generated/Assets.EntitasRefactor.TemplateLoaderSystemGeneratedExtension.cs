namespace Entitas {
    public partial class Pool {
        public ISystem CreateTemplateLoaderSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.TemplateLoaderSystem>();
        }
    }
}