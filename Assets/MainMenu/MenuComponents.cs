using System;
using Entitas;
using Entitas.CodeGenerator;
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
    public class ActivateActionComponent : IComponent
    {
        public Action Action;
    }

    [Menu]
    public class TextComponent : IComponent
    {
        public Text Text;
    }

    [Menu]
    public class SelectedComponent : IComponent
    {
    }
}
