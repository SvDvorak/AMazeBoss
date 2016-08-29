using System;
using Assets.Camera;
using Assets.Input;
using Assets.Items;
using Assets.Level;
using Assets.Render;
using Entitas;

namespace Assets
{
    public static class SystemsExtensions
    {
        public static void AddSystems(this Systems systems, Pool pool, Action<SystemsSetup> setupAction)
        {
            setupAction(new SystemsSetup(systems, pool));
        }
    }

    public class SystemsSetup
    {
        private readonly Systems _systems;
        private readonly Pool _pool;

        public SystemsSetup(Systems systems, Pool pool)
        {
            _systems = systems;
            _pool = pool;
        }

        public SystemsSetup InputSystems()
        {
            _systems
                .Add(_pool.CreateSystem<PlayerRestartSystem>())
                .Add(_pool.CreateSystem<ReturnToPreviousViewSystem>())
                .Add(_pool.CreateSystem<RotateCameraInputSystem>())
                .Add(_pool.CreateSystem<HeroInputSystem>())
                .Add(_pool.CreateSystem<PerformInputQueueSystem>());

            return this;
        }

        public SystemsSetup UpdateSystems()
        {
            _systems
                .Add(_pool.CreateSystem<PositionsCacheUpdateSystem>())
                .Add(_pool.CreateSystem<NextTurnSystem>())
                .Add(_pool.CreateSystem<BossMoveSystem>())
                .Add(_pool.CreateSystem<HeroMoveSystem>())
                .Add(_pool.CreateSystem<HeroPullBoxSystem>())
                .Add(_pool.CreateSystem<HeroItemSystem>())
                .Add(_pool.CreateSystem<HeroCurseSystem>())
                .Add(_pool.CreateSystem<SpikeTrapSystem>())
                .Add(_pool.CreateSystem<KnockBoxSystem>())
                .Add(_pool.CreateSystem<DeathSystem>())
                .Add(_pool.CreateSystem<ExitGateSystem>());

            return this;
        }

        public SystemsSetup RenderSystems()
        {
            _systems
                .Add(_pool.CreateSystem<TemplateLoaderSystem>())
                .Add(_pool.CreateSystem<SubtypeSelectorSystem>())
                .Add(_pool.CreateSystem<TemplateSelectorSystem>())
                .Add(_pool.CreateSystem<AddOrRemoveViewSystem>())
                .Add(_pool.CreateSystem<SetInitialTransformSystem>())
                .Add(_pool.CreateSystem<CameraFollowSystem>())
                .Add(_pool.CreateSystem<MoveAndRotateCameraSystem>());

            return this;
        }

        public SystemsSetup AnimationSystems()
        {
            _systems
                .Add(_pool.CreateSystem<RotationAnimationSystem>())
                .Add(_pool.CreateSystem<MoveAnimationSystem>())
                .Add(_pool.CreateSystem<BumpIntoObjectAnimationSystem>())
                .Add(_pool.CreateSystem<EdgeRecoverAnimationSystem>())
                .Add(_pool.CreateSystem<ItemCarryAnimationSystem>())
                .Add(_pool.CreateSystem<TrapLoadedAnimationSystem>())
                .Add(_pool.CreateSystem<TrapActivatedAnimationSystem>())
                .Add(_pool.CreateSystem<HealthChangedAnimationSystem>())
                .Add(_pool.CreateSystem<BoxMovedAnimationSystem>())
                .Add(_pool.CreateSystem<AttackAnimationSystem>())
                .Add(_pool.CreateSystem<DeathAnimationSystem>())
                .Add(_pool.CreateSystem<ExitGateAnimationSystem>())
                .Add(_pool.CreateSystem<CurseAnimationSystem>())
                .Add(_pool.CreateSystem<UpdateActingSystem>());

            return this;
        }

        public SystemsSetup LevelHandlingSystems(PuzzleLayout layout)
        {
            _systems
                .Add(_pool.CreateSystem(new GameLevelLoaderSystem(layout)))
                .Add(_pool.CreateSystem<FinishedLevelLoadSystem>())
                .Add(_pool.CreateSystem<LevelExitSystem>())
                .Add(_pool.CreateSystem<LevelRestartSystem>());

            return this;
        }

        public SystemsSetup DestroySystems()
        {
            _systems
                .Add(_pool.CreateSystem<CleanupSystem>())
                .Add(_pool.CreateSystem<DestroySystem>());

            return this;
        }
    }
}