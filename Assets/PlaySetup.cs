using System;
using System.Collections.Generic;
using Assets.Camera;
using Assets.FileOperations;
using Assets.Input;
using Assets.LevelEditor.Input;
using Assets.Render;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    public class PlaySetup : MonoBehaviour
    {
        public static bool FromEditor;
        public static bool FromSave;
        public static string LevelPath;
        public static Level EditorLevel;
        public static Level LevelSave;

        public List<string> Levels; 

        private Systems _systems;
        private Pool _gamePool;

        public void Start()
        {
            Random.seed = 42;
            SceneSetup.CurrentScene = "Play";
            SceneSetup.OnSceneChanging += OnSceneChanging;

            _gamePool = Pools.game;
            _systems = CreateSystems(_gamePool);

            _gamePool.SetObjectPositionCache(new Dictionary<TilePos, List<Entity>>());
            _gamePool.SetLevels(Levels);

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public void OnDestroy()
        {
            _systems.ClearReactiveSystems();
            _gamePool.Reset();
            SceneSetup.OnSceneChanging -= OnSceneChanging;
        }

        private void OnSceneChanging()
        {
            _gamePool.SafeDeleteAll();
        }

        public Systems CreateSystems(Pool pool)
        {
            return SceneSetup.CreateSystem()

            // Initialize
                .Add(pool.CreateSystem<PositionsCacheUpdateSystem>())
                .Add(pool.CreateSystem<PlayLevelLoaderSystem>())
                .Add(pool.CreateSystem<TemplateLoaderSystem>())

            // Input
                .Add(pool.CreateSystem<PlayerRestartSystem>())
                .Add(pool.CreateSystem<ReturnToPreviousViewSystem>())
                .Add(pool.CreateSystem<RotateCameraInputSystem>())
                .Add(pool.CreateSystem<HeroInputSystem>())
                .Add(pool.CreateSystem<PerformInputQueueSystem>())

            // Update
                .Add(pool.CreateSystem<NextTurnSystem>())
                .Add(pool.CreateSystem<BottomSpawnerSystem>())
                .Add(pool.CreateSystem<BossMoveSystem>())
                .Add(pool.CreateSystem<HeroMoveSystem>())
                .Add(pool.CreateSystem<HeroPullBoxSystem>())
                .Add(pool.CreateSystem<HeroItemSystem>())
                .Add(pool.CreateSystem<HeroCurseSystem>())
                .Add(pool.CreateSystem<SpikeTrapSystem>())
                .Add(pool.CreateSystem<CurseSwitchSystem>())
                .Add(pool.CreateSystem<KnockBoxSystem>())
                .Add(pool.CreateSystem<DeathSystem>())
                .Add(pool.CreateSystem<ExitGateSystem>())

            // Render
                .Add(pool.CreateSystem<SubtypeSelectorSystem>())
                .Add(pool.CreateSystem<TemplateSelectorSystem>())
                .Add(pool.CreateSystem<AddOrRemoveViewSystem>())
                .Add(pool.CreateSystem<SetInitialTransformSystem>())
                .Add(pool.CreateSystem<CameraFollowSystem>())
                .Add(pool.CreateSystem<MoveAndRotateCameraSystem>())
                .AddAnimationSystems(pool)

                .Add(pool.CreateSystem<UpdateActingSystem>())

            // Level-handling
                .Add(pool.CreateSystem<FinishedLevelLoadSystem>())
                .Add(pool.CreateSystem<SetCheckpointSystem>())
                .Add(pool.CreateSystem<LevelExitSystem>())
                .Add(pool.CreateSystem<LevelRestartSystem>())

            // Destroy
                .Add(pool.CreateSystem<CleanupSystem>())
                .Add(pool.CreateSystem<DestroySystem>());
        }
    }

    public static class PoolAnimationSystemsExtensions
    {
        public static Systems AddAnimationSystems(this Systems systems, Pool pool)
        {
            return systems
                .Add(pool.CreateSystem<MoveAnimationSystem>())
                .Add(pool.CreateSystem<TrapLoadedAnimationSystem>())
                .Add(pool.CreateSystem<TrapActivatedAnimationSystem>())
                .Add(pool.CreateSystem<CurseSwitchActivatedAnimationSystem>())
                .Add(pool.CreateSystem<HealthChangedAnimationSystem>())
                .Add(pool.CreateSystem<BoxKnockAnimationSystem>())
                .Add(pool.CreateSystem<AttackAnimationSystem>())
                .Add(pool.CreateSystem<DeathAnimationSystem>())
                .Add(pool.CreateSystem<ExitGateAnimationSystem>())
                .Add(pool.CreateSystem<CurseAnimationSystem>());
        }
    }
}