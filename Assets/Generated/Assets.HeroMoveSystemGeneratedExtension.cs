namespace Entitas {
    public partial class Pool {
        public ISystem CreateHeroMoveSystem() {
            return this.CreateSystem<Assets.HeroMoveSystem>();
        }
    }
}