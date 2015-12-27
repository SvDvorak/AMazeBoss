using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.LevelEditor.Placeables;
using Entitas;
using Entitas.CodeGenerator;

namespace Assets.LevelEditor
{
    [SingleEntity]
    public class PausedComponent : IComponent
    {
    }

    public class PlaceableSelectedComponent : IComponent
    {
        public Placeable Value;
    }

    [SingleEntity]
    public class InputComponent : IComponent
    {
    }

    [SingleEntity]
    public class PreviewComponent : IComponent
    {
    }

}
