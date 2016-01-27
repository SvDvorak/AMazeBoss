namespace Entitas {
    public partial class Pool {
        public ISystem CreateSetFocusPointSystem() {
            return this.CreateSystem<Assets.LevelEditor.SetFocusPointSystem>();
        }
    }
}