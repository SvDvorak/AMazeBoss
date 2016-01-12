namespace Entitas {
    public partial class Pool {
        public ISystem CreateHeroItemSystem() {
            return this.CreateSystem<Assets.HeroItemSystem>();
        }
    }
}