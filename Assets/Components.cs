using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets
{
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

    [SingleEntity]
    public class TickComponent : IComponent
    {
        public float TimeLeft;
    }

    public class IsAnimating : IComponent
    {
    }

    public class DestroyedComponent : IComponent { }
}