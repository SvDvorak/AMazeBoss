using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets
{
    public class IdComponent : IComponent
    {
        public int Value;
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

    public class SpikeTrapComponent : IComponent
    {
        public bool IsLoaded;
    }

    public class SpikedTargetComponent : IComponent
    {
        public int BossId;
    }

    public class TrapActivatedComponent : IComponent
    {
    }

    public class ItemComponent : IComponent
    {
    }

    public class SpikesComponent : IComponent
    {
    }

    public class BossComponent : IComponent
    {
    }

    public class HeroComponent : IComponent
    {
    }

    public class SpikesCarried : IComponent
    {
    }

    public class Cursed : IComponent
    {
    }

    public class CurseSwitch : IComponent
    {
    }

    public class DynamicComponent : IComponent
    {
    }

    public class ActiveTurnComponent : IComponent
    {
    }

    public class HealthComponent : IComponent
    {
        public int Value;
    }

    public class HealthVisualComponent : IComponent
    {
        public TextMesh Text;
    }

    public class ActingTimeComponent : IComponent
    {
        public float TimeLeft;
        public Action OnFinished;
    }

    public class AnimatorComponent : IComponent
    {
        public Animator Value;
    }

    public class DestroyedComponent : IComponent { }

    [SingleEntity]
    public class Levels : IComponent
    {
        public List<string> Value;
    }
}