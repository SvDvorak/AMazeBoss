namespace Entitas {
    public partial class Pool {
        public ISystem CreateHeroPullBoxSystem() {
            return this.CreateSystem<Assets.HeroPullBoxSystem>();
        }
    }
}