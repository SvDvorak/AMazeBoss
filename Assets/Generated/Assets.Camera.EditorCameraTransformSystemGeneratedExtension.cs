namespace Entitas {
    public partial class Pool {
        public ISystem CreateEditorCameraTransformSystem() {
            return this.CreateSystem<Assets.Camera.EditorCameraTransformSystem>();
        }
    }
}