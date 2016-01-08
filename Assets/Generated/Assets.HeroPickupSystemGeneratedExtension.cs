namespace Entitas {
    public partial class Pool {
        public ISystem CreateHeroPickupSystem() {
            return this.CreateSystem<Assets.HeroPickupSystem>();
        }
    }
}