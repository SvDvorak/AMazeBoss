using System;
using System.Collections.Generic;
using System.Linq;
using Assets.EntitasRefactor.Input;
using Assets.EntitasRefactor.Placeables;
using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    [SingleEntity]
    public class PausedComponent : IComponent
    {
    }

    public class PlaceableSelectedComponent : IComponent
    {
        public IPlaceable Value;
    }

    [SingleEntity]
    public class InputComponent : IComponent
    {
    }

    [SingleEntity]
    public class PreviewComponent : IComponent
    {
    }

    public class ParentComponent : IComponent
    {
        public int Id;
    }

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

    public class ChildComponent : IComponent
    {
        public int ParentId;
    }

    public class PositionComponent : IComponent
    {
        public TilePos Value;
    }

    public class RotationComponent : IComponent
    {
        public int Value;

        public int GetNextRotation()
        {
            return (Value + 1)%4;
        }
    }

    public class TileComponent : IComponent
    {
        public MainTileType Type;
    }

    public class MaintypeComponent : IComponent
    {
        public string Value;
    }

    public class SubtypeComponent : IComponent
    {
        public string Value;
    }

    public class ItemComponent : IComponent
    {
        public ItemType Type;
    }

    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }

    public class DestroyedComponent : IComponent
    {
    }

    [SingleEntity]
    public class TileTemplates : IComponent
    {
        public TemplateNames Value;
    }

    public class TemplateNames : Dictionary<string, SubtemplateNames>
    {
        public Tuple<string, List<string>> Retrieve(string type)
        {
            var firstSubtypeNames = this[type.ToUpper()].First();
            return new Tuple<string, List<string>>(firstSubtypeNames.Key, firstSubtypeNames.Value);
        }

        public List<string> Retrieve(string type, string subtype)
        {
            return this[type.ToUpper()][subtype.ToUpper()];
        }
    }

    public class SubtemplateNames : Dictionary<string, List<string>>
    {
    }
}