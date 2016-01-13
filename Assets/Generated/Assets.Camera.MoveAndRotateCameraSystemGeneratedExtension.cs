namespace Entitas {
    public partial class Pool {
        public ISystem CreateMoveAndRotateCameraSystem() {
            return this.CreateSystem<Assets.Camera.MoveAndRotateCameraSystem>();
        }
    }
}