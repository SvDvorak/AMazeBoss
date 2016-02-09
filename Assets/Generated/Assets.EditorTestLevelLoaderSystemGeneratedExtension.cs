namespace Entitas {
    public partial class Pool {
        public ISystem CreateEditorTestLevelLoaderSystem() {
            return this.CreateSystem<Assets.EditorTestLevelLoaderSystem>();
        }
    }
}