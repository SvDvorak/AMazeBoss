using Assets.LevelEditor.Input;

namespace Entitas {
    public partial class Pool {
        public ISystem CreateMouseInputSystem() {
            return this.CreateSystem<MouseInputSystem>();
        }
    }
}