namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewTileTypeChangedSystem() {
            return this.CreateSystem<Assets.LevelEditor.Preview.PreviewTileTypeChangedSystem>();
        }
    }
}