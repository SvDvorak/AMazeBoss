using Assets.LevelEditor.Preview;

namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewTilePositionChangedSystem() {
            return this.CreateSystem<PreviewTilePositionChangedSystem>();
        }
    }
}