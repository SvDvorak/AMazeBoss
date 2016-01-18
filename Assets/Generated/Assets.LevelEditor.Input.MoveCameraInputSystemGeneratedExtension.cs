namespace Entitas {
    public partial class Pool {
        public ISystem CreateMoveCameraInputSystem() {
            return this.CreateSystem<Assets.LevelEditor.Input.MoveCameraInputSystem>();
        }
    }
}