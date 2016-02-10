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
            var menu = Pools.menu;
            _systems = SceneSetup.CreateSystem().Add(menu.CreateAddRemoveViewSystem());
            menu.CreateEntity().AddResource("MenuItem").AddMenuItem("New Game", canvas);

            _systems.Initialize();
        }

        public void Update()
        {
            _systems.Execute();
        }
    }
}
