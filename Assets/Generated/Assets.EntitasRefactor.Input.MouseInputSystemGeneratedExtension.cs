using Assets.Input;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateMouseInputSystem() {
            return this.CreateSystem<MouseInputSystem>();
        }
    }
}