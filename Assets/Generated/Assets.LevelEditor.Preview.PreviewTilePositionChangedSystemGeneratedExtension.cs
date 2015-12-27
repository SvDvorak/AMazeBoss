namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewTilePositionChangedSystem() {
            return this.CreateSystem<Assets.LevelEditor.Preview.PreviewTilePositionChangedSystem>();
        }
    }
}