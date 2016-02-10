using System;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets
{
    [Game]
    public class IdComponent : IComponent
    {
        public int Value;
    }

    [Game]
    public class ChildComponent : IComponent
    {
        public int ParentId;
    }

    [Game, Menu]
    public class PositionComponent : IComponent
    {
        public TilePos Value;
    }

    [Game]
    public class MovesInARow : IComponent
    {
        public int Moves;
    }

    [Game]
    public class QueuedPositionComponent : IComponent
    {
        public TilePos Value;
    }

    [Game]
    public class RotationComponent : IComponent
    {
        public int Value;

        public int GetNextRotation()
        {
            return (Value + 1)%4;
        }
    }

    [Game]
    public class MaintypeComponent : IComponent
    {
        public string Value;
    }

    [Game]
    public class SubtypeComponent : IComponent
    {
        public string Value;
    }

    [Game]
    public class TileComponent : IComponent
    {
    }

    [Game]
    public class BlockingTileComponent : IComponent
    {
    }

    [Game]
    public class SpikeTrapComponent : IComponent
    {
    }

    [Game]
    [CustomPrefix("is")]
    public class LoadedComponent : IComponent
    {
        public bool LoadedThisTurn;
    }

    [Game]
    public class TrapActivatedComponent : IComponent
    {
    }

    [Game]
    public class ItemComponent : IComponent
    {
    }

    [Game]
    public class SpikesComponent : IComponent
    {
    }

    [Game]
    public class BoxComponent : IComponent
    {
    }

    [Game]
    public class KnockedComponent : IComponent
    {
        public TilePos FromDirection;
        public bool Immediate;
    }

    [Game]
    public class CharacterComponent : IComponent
    {
    }

    [Game]
    public class BossComponent : IComponent
    {
    }

    [Game]
    public class HeroComponent : IComponent
    {
    }

    [Game]
    public class SpikesCarried : IComponent
    {
    }

    [Game]
    public class Cursed : IComponent
    {
    }

    [Game]
    public class CurseSwitch : IComponent
    {
    }

    [Game]
    public class DynamicComponent : IComponent
    {
    }

    [Game]
    public class ActiveTurnComponent : IComponent
    {
    }

    [Game]
    public class HealthComponent : IComponent
    {
        public int Value;
    }

    [Game]
    public class DeadComponent : IComponent
    {
    }

    [Game]
    public class HealthVisualComponent : IComponent
    {
        public TextMesh Text;
    }

    [Game]
    public class ActingTimeComponent : IComponent
    {
        public float TimeLeft;
    }

    [Game]
    public class QueueActingComponent : IComponent
    {
        public float Time;
        public Action Action;
    }

    public static class ActingEntityExtensions
    {
        public static bool IsActing(this Entity entity)
        {
            return entity.hasActingTime || entity.hasQueueActing;
        }
    }

    [Game]
    public class AnimatorComponent : IComponent
    {
        public Animator Value;
    }

    [Game]
    public class DestroyedComponent : IComponent
    {
    }

    [SingleEntity, Game]
    public class Levels : IComponent
    {
        public List<string> Value;
    }
}