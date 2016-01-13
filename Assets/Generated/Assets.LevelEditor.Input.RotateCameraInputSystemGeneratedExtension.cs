namespace Entitas {
    public partial class Pool {
        public ISystem CreateRotateCameraInputSystem() {
            return this.CreateSystem<Assets.LevelEditor.Input.RotateCameraInputSystem>();
        }
    }
}