using Assets.LevelEditor;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectPlaceableSystem() {
            return this.CreateSystem<SelectPlaceableSystem>();
        }
    }
}