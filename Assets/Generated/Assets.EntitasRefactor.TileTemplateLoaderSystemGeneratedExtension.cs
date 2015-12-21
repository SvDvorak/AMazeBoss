namespace Entitas {
    public partial class Pool {
        public ISystem CreateTileTemplateLoaderSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.TileTemplateLoaderSystem>();
        }
    }
}