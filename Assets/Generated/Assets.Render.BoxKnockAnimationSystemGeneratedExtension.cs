namespace Entitas {
    public partial class Pool {
        public ISystem CreateBoxKnockAnimationSystem() {
            return this.CreateSystem<Assets.Render.BoxKnockAnimationSystem>();
        }
    }
}