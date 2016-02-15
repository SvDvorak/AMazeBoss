using System;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.MainMenu
{
    public class MainMenuSetup : MonoBehaviour
    {
        private Systems _systems;
        private Pool _uiPool;

        public void Start()
        {
            SceneSetup.CurrentScene = "MainMenu";

            var canvas = GameObject.Find("Canvas");
            _uiPool = Pools.ui;

            _systems = SceneSetup.CreateSystem()
                .Add(_uiPool.CreateAddRemoveViewSystem())
                .Add(_uiPool.CreateConnectMenuItemToParentSystem())
                .Add(_uiPool.CreateCursorClickMenuItemSystem())
                .Add(_uiPool.CreateSelectedItemAnimationSystem());

            _uiPool.CreateMenuItems(canvas,
                new Tuple<string, Action>("New Game", () => SceneSetup.LoadScene("MainMenu")),
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
            foreach (var pool in Pools.allPools)
            {
                pool.Reset();
            }
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