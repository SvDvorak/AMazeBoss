using Assets.EntitasRefactor.Input;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateMouseInputSystem() {
            return this.CreateSystem<MouseInputSystem>();
        }
    }
}