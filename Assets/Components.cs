using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets
{
    [Game, Ui]
    public class SceneComponent : IComponent
    {
        public Scene Value;
    }

    public enum Scene
    {
        MainMenu,
        Play,
        Editor
    }
    
    [Game, Ui]
    public class IdComponent : IComponent
    {
        public int Value;
    }

    [Game, Ui]
    public class ChildComponent : IComponent
    {
        public int ParentId;
    }

    [SingleEntity, Game]
    public class LevelLoaded : IComponent
    {
    }

    [Game]
    public class PositionComponent : IComponent
    {
        public TilePos Value;
    }

    [SingleEntity, Game]
    public class ObjectPositionCacheComponent : IComponent
    {
        public Dictionary<TilePos, List<Entity>> Cache;
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
    public class GameObjectComponent : IComponent
    {
        public ObjectType Type;
    }

    public enum ObjectType
    {
        Tile,
        Item,
        Area
    }

    [Game]
    public class WallComponent : IComponent
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
        public float Wait;
    }

    [Game]
    public class RockedComponent : IComponent
    {
    }

    [Game]
    public class ExitGateComponent : IComponent
    {
        public bool Locked;
    }

    [Game]
    public class ExitTriggerComponent : IComponent
    {
    }

    [Game]
    public class PuzzleAreaComponent : IComponent
    {
    }

    [Game]
    public class BossConnectionComponent : IComponent
    {
        public int BossId;
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
    public class AttackingComponent : IComponent
    {
    }

    [Game]
    [CustomPrefix("Has")]
    public class BumpedIntoObject : IComponent
    {
    }

    [Game]
    public class HeroComponent : IComponent
    {
    }

    [Game]
    public class SpikesCarriedComponent : IComponent
    {
    }

    [Game]
    public class CursedComponent : IComponent
    {
    }

    [Game]
    public class PullingComponent : IComponent
    {
    }

    [Game]
    public class PushingComponent : IComponent
    {
    }

    [Game]
    public class CurseSwitchComponent : IComponent
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
    public class ActingSequencesComponent : IComponent
    {
        public Queue<ActingSequence> Sequences;
    }

    public class ActingSequence
    {
        public float TimeLeft;
        public Sequence Sequence;

        public ActingSequence(float time, Sequence sequence)
        {
            TimeLeft = time;
            Sequence = sequence;
        }
    }

    [Game]
    public class AnimatorComponent : IComponent
    {
        public Animator Value;
    }

    [Game, Ui]
    public class DestroyedComponent : IComponent
    {
    }

    [SingleEntity, Game]
    public class LevelsComponent : IComponent
    {
        public List<string> Value;
    }

    [Game]
    [CustomPrefix("Has")]
    public class SetCheckpoint : IComponent
    {
    }
}