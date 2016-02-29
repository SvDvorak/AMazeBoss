using System;
using System.Collections.Generic;
using Assets.Camera;
using Assets.FileOperations;
using Assets.LevelEditor.Input;
using Assets.LevelEditor.Preview;
using Assets.Render;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.LevelEditor
{
    public class EditorSetup : MonoBehaviour
    {
        private static EditorSetup _setup;
        private Systems _systems;
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
            Random.seed = 42;
            SceneSetup.CurrentScene = "Editor";
            SceneSetup.OnSceneChanging += OnSceneChanging;

            _gamePool = Pools.game;
            _systems = CreateGameSystems(_gamePool);

            _gamePool.isInEditor = true;
            _gamePool.SetObjectPositionCache(new Dictionary<TilePos, List<Entity>>());
            _gamePool.CreateEntity().IsInput(true);
            _gamePool.CreateEntity().IsPreview(true);
            _gamePool.CreateEntity().AddResource("Camera").AddRotation(0).AddFocusPoint(Vector3.zero).AddSavedFocusPoint(Vector3.zero);

            _systems.Initialize();
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

        public void Update()
        {
            _systems.Execute();
        }

        public Systems CreateGameSystems(Pool pool)
        {
            return SceneSetup.CreateSystem()

            // Initialize
                .Add(pool.CreateSystem<PositionsCacheUpdateSystem>())
                .Add(pool.CreateSystem<EditorLevelLoaderSystem>())
                .Add(pool.CreateSystem<TemplateLoaderSystem>())

            // Input
                .Add(pool.CreateSystem<ReturnToPreviousViewSystem>())
                .Add(pool.CreateSystem<MouseInputSystem>())
                .Add(pool.CreateSystem<MoveCameraInputSystem>())
                .Add(pool.CreateSystem<RotateCameraInputSystem>())

            // Update
                .Add(pool.CreateSystem<WallAdjustmentSystem>())
                .Add(pool.CreateSystem<SelectPlaceableSystem>())
                .Add(pool.CreateSystem<PutDownPlaceableSystem>())
                .Add(pool.CreateSystem<RemovePlaceableSystem>())
                .Add(pool.CreateSystem<SetFocusPointSystem>())
                .Add(pool.CreateSystem<BottomSpawnerSystem>())
                .Add(pool.CreateSystem<RemoveImpossiblyPlacedItemsSystem>())
                .Add(pool.CreateSystem<ViewModeChangedSystem>())
                .Add(pool.CreateSystem<ViewModeVisualAddedSystem>())
                .Add(pool.CreateSystem<PuzzleAreaExpandedSystem>())
                .Add(pool.CreateSystem<PuzzleAreaShrunkSystem>())
                .Add(pool.CreateSystem<PuzzleAreaBossRemovedSystem>())
                .Add(pool.CreateSystem<PreviewTilePositionChangedSystem>())
                .Add(pool.CreateSystem<PreviewTileTypeChangedSystem>())
                .Add(pool.CreateSystem<PreviewMaterialChangeSystem>())

            // Render
                .Add(pool.CreateSystem<SubtypeSelectorSystem>())
                .Add(pool.CreateSystem<TemplateSelectorSystem>())
                .Add(pool.CreateSystem<AddOrRemoveViewSystem>())
                .Add(pool.CreateSystem<SetInitialTransformSystem>())
                .Add(pool.CreateSystem<MoveAndRotateCameraSystem>())
                .Add(pool.CreateSystem<RenderPositionsSystem>())
                .Add(pool.CreateSystem<TrapLoadedAnimationSystem>())
                .Add(pool.CreateSystem<HealthChangedAnimationSystem>())

            // Destroy
                .Add(pool.CreateSystem<DestroySystem>());
        }
    }
}