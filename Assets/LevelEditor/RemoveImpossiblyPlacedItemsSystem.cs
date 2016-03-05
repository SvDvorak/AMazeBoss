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

        public TriggerOnEvent trigger { get { return Matcher.AnyOf(GameMatcher.GameObject).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var item in entities.Where(x => x.IsItem()))
            {
                Execute(item);
            }
        }

        private void Execute(Entity item)
        {
            var tile = _pool.GetTileAt(item.position.Value);

            if (tile != null && tile.isBlockingTile)
            {
                item.IsDestroyed(true);
            }
        }
    }
}
