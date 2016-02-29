using System.Linq;
using Entitas;

namespace Assets.LevelEditor.Preview
{
    public abstract class PreviewChangedSystem : ISetPool, IEnsureComponents
    {
        private Group _previewGroup;
        protected Pool Pool;

        public IMatcher ensureComponents { get { return GameMatcher.Input; } }

        public void SetPool(Pool pool)
        {
            _previewGroup = pool.GetGroup(Matcher.AllOf(GameMatcher.Preview));
            Pool = pool;
        }

        public Entity GetPreviewEntity()
        {
            return _previewGroup.GetEntities().Single(x => !x.isDestroyed);
        }
    }
}