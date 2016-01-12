using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

namespace Assets
{
    public class PlaySetup : MonoBehaviour
    {
        Systems _systems;

        public void Start()
        {
            Random.seed = 42;

            _systems = CreateSystems(Pools.pool);

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public void OnDestroy()
        {
            Pools.pool.ClearGroups();
            _systems.ClearReactiveSystems();
            Pools.pool.DestroyAllEntities();
        }

        public Systems CreateSystems(Pool pool)
        {
#if (UNITY_EDITOR)
            return new DebugSystems()
#else
        return new Systems()
#endif
            // Initialize
                .Add(pool.CreateTemplateLoaderSystem())

            // Update
                .Add(pool.CreateNextTurnSystem())
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreateBossMoveSystem())
                .Add(pool.CreateHeroMoveSystem())
                .Add(pool.CreateHeroItemSystem())
                .Add(pool.CreateQueuePositionSystem())
                .Add(pool.CreateSpikeTrapSystem())

                .Add(pool.CreateRemoveActingOnDoneSystem())

            // Render
                .Add(pool.CreateSubtypeSelectorSystem())
                .Add(pool.CreateTemplateSelectorSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.CreateMoveSystem())
                .AddAnimationSystems(pool)

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }

    public static class PoolAnimationSystemsExtensions
    {
        public static Systems AddAnimationSystems(this Systems systems, Pool pool)
        {
            systems.Add(pool.CreatePositionAnimationSystem());
            systems.Add(pool.CreateTrapLoadedAnimationSystem());
            systems.Add(pool.CreateTrapActivatedAnimationSystem());
            systems.Add(pool.CreateHealthChangedAnimationSystem());
            return systems;
        }
    }
}