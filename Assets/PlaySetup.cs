using System.Collections.Generic;
using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

namespace Assets
{
    public class PlaySetup : MonoBehaviour
    {
        public static bool FromEditor;
        public static string LevelPath;

        public List<string> Levels; 

        private Systems _systems;

        public void Start()
        {
            Random.seed = 42;

            var pool = Pools.pool;
            _systems = CreateSystems(pool);

            pool.SetLevels(Levels);

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public void OnDisable()
        {
            _systems.ClearReactiveSystems();
            Pools.pool.Reset();
        }

        public Systems CreateSystems(Pool pool)
        {
#if (UNITY_EDITOR)
            return new DebugSystems()
#else
        return new Systems()
#endif
            // Initialize
                .Add(pool.CreateLevelLoaderSystem())
                .Add(pool.CreateTemplateLoaderSystem())

            // Input
                .Add(pool.CreatePlayerRestartSystem())
                .Add(pool.CreateReturnToEditorSystem())
                .Add(pool.CreateRotateCameraInputSystem())
                .Add(pool.CreateHeroInputSystem())
                .Add(pool.CreatePerformInputQueueSystem())

            // Update
                .Add(pool.CreateNextTurnSystem())
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreateBossMoveSystem())
                .Add(pool.CreateHeroMoveSystem())
                .Add(pool.CreateHeroPullBoxSystem())
                .Add(pool.CreateHeroItemSystem())
                .Add(pool.CreateHeroCurseSystem())
                .Add(pool.CreateSpikeTrapSystem())
                .Add(pool.CreateCurseSwitchSystem())
                .Add(pool.CreateKnockBoxSystem())

                .Add(pool.CreateRemoveActingOnDoneSystem())

            // Render
                .Add(pool.CreateSubtypeSelectorSystem())
                .Add(pool.CreateTemplateSelectorSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.CreateMoveAndRotateCameraSystem())
                .AddAnimationSystems(pool)

            // Level-handling
                .Add(pool.CreateLevelClearedSystem())
                .Add(pool.CreateLevelRestartSystem())

            // Destroy
                .Add(pool.CreateCleanupSystem())
                .Add(pool.CreateDestroySystem());
        }
    }

    public static class PoolAnimationSystemsExtensions
    {
        public static Systems AddAnimationSystems(this Systems systems, Pool pool)
        {
            return systems
                .Add(pool.CreateMoveAnimationSystem())
                .Add(pool.CreateTrapLoadedAnimationSystem())
                .Add(pool.CreateTrapActivatedAnimationSystem())
                .Add(pool.CreateCurseSwitchActivatedAnimationSystem())
                .Add(pool.CreateHealthChangedAnimationSystem())
                .Add(pool.CreateBoxKnockAnimationSystem())
                .Add(pool.CreateDeathAnimationSystem())
                .Add(pool.CreateCurseAnimationSystem());
        }
    }
}