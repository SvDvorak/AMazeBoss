using Assets;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateTemplateSelectorSystem() {
            return this.CreateSystem<TemplateSelectorSystem>();
        }
    }
}