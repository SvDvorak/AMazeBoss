using System;
using System.Linq;
using Assets.Render;
using Entitas;
using UnityEngine;

namespace Assets.MainMenu
{
    public class ReloadSystem : IExecuteSystem
    {
        public void Execute()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                SceneSetup.LoadScene("MainMenu");
            }
        }
    }
    public class MainMenuSetup : MonoBehaviour
    {
        private Systems _systems;
        private Pool _uiPool;

        public void Start()
        {
            SceneSetup.CurrentScene = "MainMenu";
            SceneSetup.OnSceneChanging += OnSceneChanging;

            var canvas = GameObject.Find("Canvas");
            _uiPool = Pools.ui;

            _systems = SceneSetup.CreateSystem().Add<ReloadSystem>()
                .Add(_uiPool.CreateSystem<AddViewSystem>())
                .Add(_uiPool.CreateSystem<ConnectMenuItemToParentSystem>())
                .Add(_uiPool.CreateSystem<CursorClickMenuItemSystem>())
                .Add(_uiPool.CreateSystem<SelectedItemAnimationSystem>())
                .Add(_uiPool.CreateSystem<DestroySystem>());

            _uiPool.CreateMenuItems(canvas,
                new Tuple<string, Action>("New Game", () => SceneSetup.LoadScene("Play")),
                new Tuple<string, Action>("Editor", () => SceneSetup.LoadScene("Editor")));

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public void OnDestroy()
        {
            _systems.ClearReactiveSystems();
            _uiPool.Reset();
            SceneSetup.OnSceneChanging -= OnSceneChanging;
        }

        private void OnSceneChanging()
        {
            _uiPool.SafeDeleteAll();
        }
    }

    public static class MenuItemPoolExtensions
    {
        public static void CreateMenuItems(this Pool pool, GameObject parent, params Tuple<string, Action>[] items)
        {
            for (var orderNumber = 0; orderNumber < items.Count(); orderNumber++)
            {
                var itemSettings = items[orderNumber];
                pool.CreateEntity()
                    .AddResource("MenuItem")
                    .AddMenuItem(itemSettings.Item1, parent)
                    .AddActivateAction(itemSettings.Item2)
                    .AddId(orderNumber);
            }
        }
    }
}