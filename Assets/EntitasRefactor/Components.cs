using System;
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

    public class MaintypeComponent : IComponent
    {
        public string Value;
    }

    public class SubtypeComponent : IComponent
    {
        public string Value;
    }

    public class TileComponent : IComponent
    {
    }

    public class WalkableComponent : IComponent
    {
    }

    public class ItemComponent : IComponent
    {
    }

    public class BossComponent : IComponent
    {
    }

    public class HeroComponent : IComponent { }

    public class ThinkDelayComponent : IComponent
    {
        public float TimeLeft;
    }

    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }

    public class DestroyedComponent : IComponent { }
}