using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

namespace Assets.LevelEditor
{
    public class RemoveImpossiblyPlacedItemsSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AnyOf(GameMatcher.Item, GameMatcher.Tile).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                Execute(entity);
            }
        }

        private void Execute(Entity entity)
        {
            var item = _pool.GetItemAt(entity.position.Value);
            var tile = _pool.GetTileAt(entity.position.Value);

            if (item != null && tile != null)
            {
                if (tile.isBlockingTile)
                {
                    item.IsDestroyed(true);
                }
            }
        }
    }
}
