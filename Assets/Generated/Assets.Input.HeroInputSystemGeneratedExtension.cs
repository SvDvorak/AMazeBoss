namespace Entitas {
    public partial class Pool {
        public ISystem CreateHeroInputSystem() {
            return this.CreateSystem<Assets.Input.HeroInputSystem>();
        }
    }
}