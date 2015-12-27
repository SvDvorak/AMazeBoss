using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public static class EntityExtensions
    {
        private static int _freeParentId;
        private static int FreeParentId { get { return _freeParentId++; } }

        public static void AddParent(this Entity entity)
        {
            entity.AddParent(FreeParentId);
        }

        public static Entity FindChildFor(this Pool pool, Entity entity)
        {
            return pool.FindChildrenFor(entity).SingleEntity();
        }

        public static List<Entity> FindChildrenFor(this Pool pool, Entity entity)
        {
            return pool
                .GetEntities(Matcher.Child)
                .Where(x => x.child.ParentId == entity.parent.Id)
                .ToList();
        }
    }
}