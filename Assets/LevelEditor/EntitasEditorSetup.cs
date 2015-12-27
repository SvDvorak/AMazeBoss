using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class EntitasEditorSetup : MonoBehaviour
    {
        private Systems _systems;

        public static bool IsInEditor;

        public void Start()
        {
            IsInEditor = true;
            SetupEntitas();
            LoadLevel();
        }

        private static void LoadLevel()
        {
            var lastUsedPath = FileOperations.FileOperations.GetLastUsedPath();

            if (!string.IsNullOrEmpty(lastUsedPath))
            {
                FileOperations.FileOperations.Load(lastUsedPath);
            }
        }

        public void OnDestroy()
        {
            IsInEditor = false;
            _systems.DeactivateReactiveSystems();
            Pools.pool.DestroyAllEntities();
        }

        private void SetupEntitas()
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
                .Add(pool.CreateTemplateLoaderSystem())

            // Input
                .Add(pool.CreateMouseInputSystem())

            // Update
                .Add(pool.CreateSelectPlaceableSystem())
                .Add(pool.CreatePutDownPlaceableSystem())
                .Add(pool.CreateRemoveObjectSystem())
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreateWallAdjustmentSystem())
                .Add(pool.CreatePreviewTilePositionChangedSystem())
                .Add(pool.CreatePreviewTileTypeChangedSystem())
                .Add(pool.CreatePreviewMaterialChangeSystem())

            // Render
                .Add(pool.CreateTemplateSelectorSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.CreateRenderPositionsSystem())

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }
}