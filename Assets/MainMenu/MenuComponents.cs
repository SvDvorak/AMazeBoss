using Entitas;
using UnityEngine;

namespace Assets.MainMenu
{
    [Menu]
    public class MenuItem : IComponent
    {
        public string Text;
        public GameObject Parent;
    }
}
