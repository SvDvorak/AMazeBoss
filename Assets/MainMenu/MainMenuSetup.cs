using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

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
                .Add(menuPool.CreateConnectMenuItemToParentSystem());

            menuPool.CreateMenuItems(canvas, "New Game", "Editor");

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }
    }

    public static class MenuItemPoolExtensions
    {
        public static void CreateMenuItems(this Pool pool, GameObject parent, params string[] names)
        {
            for (var orderNumber = 0; orderNumber < names.Count(); orderNumber++)
            {
                pool.CreateEntity()
                    .AddResource("MenuItem")
                    .AddMenuItem(names[orderNumber], parent)
                    .AddId(orderNumber);
            }
        }
    }
}