using Assets.LevelEditor.Preview;

namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewTileTypeChangedSystem() {
            return this.CreateSystem<PreviewTileTypeChangedSystem>();
        }
    }
}