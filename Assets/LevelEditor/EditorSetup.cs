using System;
using Assets.FileOperations;
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
        private Pool _gamePool;

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

        private void SetupEntitas()
        {
            Random.seed = 42;

            _gamePool = Pools.game;
            _systems = CreateSystems(_gamePool);

            _gamePool.CreateEntity().IsInput(true);
            _gamePool.CreateEntity().IsPreview(true);
            _gamePool.CreateEntity().AddResource("Camera").AddRotation(0).AddFocusPoint(Vector3.zero).AddSavedFocusPoint(Vector3.zero);

            _systems.Initialize();
        }

        private static void LoadLevel()
        {
            var lastUsedLevelName = PlayerPrefsLevelReader.LastUsedLevelName;

            try
            {
                PlayerPrefsLevelReader.LoadLevel(lastUsedLevelName);
            }
            catch (Exception)
            {
                Debug.LogWarning("Unable to read last used level " + lastUsedLevelName);
            }
        }

        public void OnDestroy()
        {
            _systems.ClearReactiveSystems();
            _gamePool.Reset();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public Systems CreateSystems(Pool pool)
        {
            return SceneSetup.CreateSystem()

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
                .Add(pool.CreateSetFocusPointSystem())
                .Add(pool.CreateBottomSpawnerSystem())
                .Add(pool.CreateRemoveImpossiblyPlacedItemsSystem())
                .Add(pool.CreatePreviewTilePositionChangedSystem())
                .Add(pool.CreatePreviewTileTypeChangedSystem())
                .Add(pool.CreatePreviewMaterialChangeSystem())

            // Render
                .Add(pool.CreateSubtypeSelectorSystem())
                .Add(pool.CreateTemplateSelectorSystem())
                .Add(pool.CreateAddRemoveViewSystem())
                .Add(pool.CreateSetInitialTransformSystem())
                .Add(pool.CreateMoveAndRotateCameraSystem())
                .Add(pool.CreateRenderPositionsSystem())
                .Add(pool.CreateTrapLoadedAnimationSystem())
                .Add(pool.CreateHealthChangedAnimationSystem())

            // Destroy
                .Add(pool.CreateDestroySystem());
        }
    }
}