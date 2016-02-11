using System;
using System.Linq;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.MainMenu
{
    public class MainMenuSetup : MonoBehaviour
    {
        private Systems _systems;

        public void Start()
        {
            var canvas = GameObject.Find("Canvas");
            var menuPool = Pools.menu;

            _systems = SceneSetup.CreateSystem()
                .Add(menuPool.CreateAddRemoveViewSystem())
                .Add(menuPool.CreateConnectMenuItemToParentSystem())
                .Add(menuPool.CreateCursorClickMenuItemSystem())
                .Add(menuPool.CreateSelectedItemAnimationSystem());

            menuPool.CreateMenuItems(canvas,
                new Tuple<string, Action>("New Game", () => SceneManager.LoadScene("Play")),
                new Tuple<string, Action>("Editor", () => SceneManager.LoadScene("Editor")));

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
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