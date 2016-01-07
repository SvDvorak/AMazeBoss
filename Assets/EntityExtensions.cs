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

        public static bool IsMoving(this Entity entity)
        {
            var target = entity.position.Value.ToV3();
            var current = entity.view.Value.transform.position;
            return entity.hasPosition && entity.hasView && target != current;
        }
    }
}