using Assets;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateBossMoveSystem() {
            return this.CreateSystem<BossMoveSystem>();
        }
    }
}