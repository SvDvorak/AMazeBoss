namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewMaterialChangeSystem() {
            return this.CreateSystem<Assets.Render.PreviewMaterialChangeSystem>();
        }
    }
}