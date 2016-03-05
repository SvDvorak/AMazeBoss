using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor
{
    public class RemovePlaceableSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.InputRemove, GameMatcher.Position).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.Input; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var input = entities.SingleEntity();
            var tilePosition = input.position.Value;

            if (_pool.editorViewMode.Value == ViewMode.Area)
            {
                RemoveArea(tilePosition);
            }
            else
            {
                RemoveTile(tilePosition);
            }
        }

        private void RemoveArea(TilePos tilePosition)
        {
            TryRemove(_pool.GetAreaAt(tilePosition));
        }

        private void RemoveTile(TilePos tilePos)
        {
            if (TryRemove(_pool.GetItemAt(tilePos)))
            {
                return;
            }

            TryRemove(_pool.GetTileAt(tilePos));
        }

        private bool TryRemove(Entity entity)
        {
            if (entity != null)
            {
                entity.IsDestroyed(true);
                return true;
            }

            return false;
        }
    }
}