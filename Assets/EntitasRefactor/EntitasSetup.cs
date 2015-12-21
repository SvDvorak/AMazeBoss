using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    public class EntitasSetup : MonoBehaviour
    {
        Systems _systems;

        public void Start()
        {
            Random.seed = 42;

            _systems = CreateSystems(Pools.pool);

            Pools.pool.CreateEntity().IsInput(true);
            Pools.pool.CreateEntity().IsPreview(true);

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public Systems CreateSystems(Pool pool)
        {
#if (UNITY_EDITOR)
            return new DebugSystems()
#else
        return new Systems()
#endif
            // Initialize
                .Add(pool.CreateTileTemplateLoaderSystem())

            // Input
                .Add(pool.CreateMouseInputSystem())

            // Update
                .Add(pool.CreateSelectTileSystem())
                .Add(pool.CreatePlaceTileSystem())
                .Add(pool.CreateRemoveTileSystem())
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreatePreviewTilePositionChangedSystem())
                .Add(pool.CreatePreviewTileTypeChangedSystem())

            // Render
                .Add(pool.CreateTileTemplateSelectorSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.CreateRenderPositionsSystem())

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }
}