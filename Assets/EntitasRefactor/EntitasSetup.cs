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

                .Add(pool.CreateMouseInputSystem())
                .Add(pool.CreateSelectTileSystem())
                .Add(pool.CreatePlaceTileSystem())
                .Add(pool.CreatePreviewTilePositionChangedSystem())
                .Add(pool.CreatePreviewTileTypeChangedSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.CreateRenderPositionsSystem());
            // Initialize
            //.Add(pool.CreateSystem<CreatePlayerSystem>())
            //.Add(pool.CreateSystem<CreateOpponentsSystem>())
            //.Add(pool.CreateSystem<CreateFinishLineSystem>())

            // Input
            //.Add(pool.CreateSystem<InputSystem>())

            // Update
            //.Add(pool.CreateSystem<AccelerateSystem>())
            //.Add(pool.CreateSystem<MoveSystem>())
            //.Add(pool.CreateSystem<ReachedFinishSystem>())

            // Destroy
            //.Add(pool.CreateSystem<DestroySystem>());
        }
    }
}