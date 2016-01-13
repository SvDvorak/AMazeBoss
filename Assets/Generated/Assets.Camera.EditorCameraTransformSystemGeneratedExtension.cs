namespace Entitas {
    public partial class Pool {
        public ISystem EditorCameraTransformSystem() {
            return this.CreateSystem<Assets.Camera.EditorCameraTransformSystem>();
        }
    }
}