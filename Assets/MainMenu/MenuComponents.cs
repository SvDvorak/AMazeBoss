using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu
{
    [Menu]
    public class MenuItemComponent : IComponent
    {
        public string Text;
        public GameObject Parent;
    }

    [Menu]
    public class TextComponent : IComponent
    {
        public Text Text;
    }
}
