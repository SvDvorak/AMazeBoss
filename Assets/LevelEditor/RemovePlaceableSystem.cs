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

            RemoveTile(tilePosition);
        }

        private void RemoveTile(TilePos tilePos)
        {
            var selectedItem = _pool.GetItemAt(tilePos);
            if (selectedItem != null)
            {
                selectedItem.IsDestroyed(true);
                return;
            }

            var selectedTile = _pool.GetTileAt(tilePos);
            if (selectedTile != null)
            {
                selectedTile.IsDestroyed(true);
            }
        }
    }
}