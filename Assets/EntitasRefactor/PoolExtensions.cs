using System.Linq;
using Entitas;

namespace Assets.EntitasRefactor
{
    public static class PoolExtensions
    {
        public static Entity GetTileAt(this Pool pool, TilePos position)
        {
            var entities = pool.GetEntities(Matcher.AllOf(Matcher.Tile, Matcher.Position));
            return entities.SingleOrDefault(x => x.position.Value == position);
        }
    }
}