using Assets.LevelEditor;

namespace Entitas {
    public partial class Pool {
        public ISystem CreatePutDownPlaceableSystem() {
            return this.CreateSystem<PutDownPlaceableSystem>();
        }
    }
}