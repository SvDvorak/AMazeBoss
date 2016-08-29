using System;
using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MainMenu
{
    [Ui]
    public class MenuItemComponent : IComponent
    {
        public string Text;
        public GameObject Parent;
    }

    [Ui]
    public class ActivateActionComponent : IComponent
    {
        public Action Action;
    }

    [Ui]
    public class TextComponent : IComponent
    {
        public Text Text;
    }

    [Ui]
    public class SelectedComponent : IComponent
    {
    }
}
