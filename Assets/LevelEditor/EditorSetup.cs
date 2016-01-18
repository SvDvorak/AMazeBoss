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

        private static EditorSetup _setup;

        public static EditorSetup Instance
        {
            get
            {
                if (_setup == null)
                {
                    _setup = GameObject.Find("Setup").GetComponent<EditorSetup>();
                }
                return _setup;
            }
            set
            {
                _setup = value;
            }
        }

        public void Start()
        {
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
            _systems.ClearReactiveSystems();
            Pools.pool.ClearGroups();
            Pools.pool.DestroyAllEntities();
        }

        private void SetupEntitas()
        {
            Random.seed = 42;

            var pool = Pools.pool;
            _systems = CreateSystems(pool);

            pool.CreateEntity().IsInput(true);
            pool.CreateEntity().IsPreview(true);
            pool.CreateEntity().AddResource("Camera").AddFocusPoint(Vector3.zero).AddRotation(0);

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
                .Add(pool.CreateMoveCameraInputSystem())
                .Add(pool.CreateRotateCameraInputSystem())

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
                .Add(pool.CreateMoveAndRotateCameraSystem())
                .Add(pool.CreateRenderPositionsSystem())
                .Add(pool.CreateTrapLoadedAnimationSystem())
                .Add(pool.CreateHealthChangedAnimationSystem())

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }
}