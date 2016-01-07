using Entitas;
using Entitas.CodeGenerator;
using UnityEditor.Animations;
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

    public class QueuedPositionComponent : IComponent
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

    public class HeroComponent : IComponent
    {
    }

    public class DynamicComponent : IComponent
    {
    }

    public class ActiveTurnComponent : IComponent
    {
    }

    public class ActingTimeComponent : IComponent
    {
        public float TimeLeft;
    }

    public class AnimatorComponent : IComponent
    {
        public Animator Value;
    }

    public class DestroyedComponent : IComponent { }
}