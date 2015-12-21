using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    public class TileSelectComponent : IComponent
    {
        public MainTileType Type;
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
    }

    public class TileComponent : IComponent
    {
        public MainTileType Type;
    }

    public class SubtypeComponent : IComponent
    {
        public string Value;
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

    public class TemplateNames : Dictionary<MainTileType, SubtemplateNames>
    {
        private KeyValuePair<string, List<string>> _firstSubtypeNames;

        public Tuple<string, List<string>> Retrieve(MainTileType type)
        {
            _firstSubtypeNames = this[type].First();
            return new Tuple<string, List<string>>(_firstSubtypeNames.Key, _firstSubtypeNames.Value);
        }

        public List<string> Retrieve(MainTileType type, string subtype)
        {
            return this[type][subtype];
        }
    }

    public class SubtemplateNames : Dictionary<string, List<string>>
    {
    }
}