using Assets.Render;

namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewMaterialChangeSystem() {
            return this.CreateSystem<PreviewMaterialChangeSystem>();
        }
    }
}