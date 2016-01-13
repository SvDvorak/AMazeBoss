using System;
using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.LevelEditor
{
    public class EditorSetup : MonoBehaviour
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

            try
            {
                FileOperations.FileOperations.Load(lastUsedPath);
            }
            catch (Exception)
            {
                Debug.LogWarning("Unable to read last used file at " + lastUsedPath);
            }
        }

        public void OnDestroy()
        {
            IsInEditor = false;
            Pools.pool.ClearGroups();
            _systems.ClearReactiveSystems();
            Pools.pool.DestroyAllEntities();
        }

        private void SetupEntitas()
        {
            Random.seed = 42;

            var pool = Pools.pool;
            _systems = CreateSystems(pool);

            pool.CreateEntity().IsInput(true);
            pool.CreateEntity().IsPreview(true);
            pool.CreateEntity().AddResource("Camera").AddCameraOffset(Vector3.zero).AddRotation(0);

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
                .Add(pool.CreateWallAdjustmentSystem())
                .Add(pool.CreateSelectPlaceableSystem())
                .Add(pool.CreatePutDownPlaceableSystem())
                .Add(pool.CreateRemovePlaceableSystem())
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreateRemoveImpossiblyPlacedItemsSystem())
                .Add(pool.CreatePreviewTilePositionChangedSystem())
                .Add(pool.CreatePreviewTileTypeChangedSystem())
                .Add(pool.CreatePreviewMaterialChangeSystem())

            // Render
                .Add(pool.CreateSubtypeSelectorSystem())
                .Add(pool.CreateTemplateSelectorSystem())
                .Add(pool.CreateAddViewSystem())
                .Add(pool.EditorCameraTransformSystem())
                .Add(pool.CreateRenderPositionsSystem())

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }
}