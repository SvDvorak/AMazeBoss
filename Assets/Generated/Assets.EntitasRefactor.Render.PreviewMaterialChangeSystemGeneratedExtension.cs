namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewMaterialChangeSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Render.PreviewMaterialChangeSystem>();
        }
    }
}