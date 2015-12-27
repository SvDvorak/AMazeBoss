using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

namespace Assets
{
    public class EntitasPlaySetup : MonoBehaviour
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
            _systems.DeactivateReactiveSystems();
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
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreateBossMoveSystem())
                .Add(pool.CreateHeroMoveSystem())

            // Render
                .Add(pool.CreateTemplateSelectorSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.CreateRenderPositionsSystem())

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }
}