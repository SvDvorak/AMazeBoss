using Assets;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateTemplateLoaderSystem() {
            return this.CreateSystem<TemplateLoaderSystem>();
        }
    }
}