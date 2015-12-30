namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveImpossiblyPlacedItemsSystem() {
            return this.CreateSystem<Assets.LevelEditor.RemoveImpossiblyPlacedItemsSystem>();
        }
    }
}