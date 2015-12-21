using Entitas;

namespace Assets.EntitasRefactor.Preview
{
    public abstract class PreviewChangedSystem : ISetPool, IEnsureComponents
    {
        protected Group PreviewGroup;

        public IMatcher ensureComponents { get { return Matcher.Input; } }

        public void SetPool(Pool pool)
        {
            PreviewGroup = pool.GetGroup(Matcher.Preview);
        }

        public Entity GetPreviewEntity()
        {
            return PreviewGroup.GetSingleEntity();
        }
    }
}