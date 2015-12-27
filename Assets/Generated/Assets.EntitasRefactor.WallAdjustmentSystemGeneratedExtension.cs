using Assets.LevelEditor;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateWallAdjustmentSystem() {
            return this.CreateSystem<WallAdjustmentSystem>();
        }
    }
}