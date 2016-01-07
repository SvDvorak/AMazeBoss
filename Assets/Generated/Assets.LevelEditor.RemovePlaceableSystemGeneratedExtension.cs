namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemovePlaceableSystem() {
            return this.CreateSystem<Assets.LevelEditor.RemovePlaceableSystem>();
        }
    }
}