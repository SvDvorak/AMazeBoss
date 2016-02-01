namespace Entitas {
    public partial class Pool {
        public ISystem CreateHeroCurseSystem() {
            return this.CreateSystem<Assets.HeroCurseSystem>();
        }
    }
}