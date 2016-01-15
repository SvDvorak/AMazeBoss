namespace Entitas {
    public partial class Pool {
        public ISystem CreateReturnToEditorSystem() {
            return this.CreateSystem<Assets.ReturnToEditorSystem>();
        }
    }
}