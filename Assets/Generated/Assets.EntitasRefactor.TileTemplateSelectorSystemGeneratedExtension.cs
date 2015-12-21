namespace Entitas {
    public partial class Pool {
        public ISystem CreateTileTemplateSelectorSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.TileTemplateSelectorSystem>();
        }
    }
}