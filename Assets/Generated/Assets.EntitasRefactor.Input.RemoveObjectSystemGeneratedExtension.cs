using Assets.LevelEditor;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveObjectSystem() {
            return this.CreateSystem<RemovePlaceableSystem>();
        }
    }
}