using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor.Placeables;
using Entitas;
using Entitas.CodeGenerator;

namespace Assets.LevelEditor
{
    [SingleEntity]
    public class PausedComponent : IComponent
    {
    }

    public class SelectedPlaceableComponent : IComponent
    {
        public IPlaceable Value;
    }

    [SingleEntity]
    public class SelectedPlaceablesGroupComponent : IComponent
    {
        public SelectionGroup Group;
    }

    [SingleEntity]
    public class InputComponent : IComponent
    {
    }

    [SingleEntity]
    public class PreviewComponent : IComponent
    {
    }

    [SingleEntity]
    public class InputPlaceComponent : IComponent
    {
    }

    [SingleEntity]
    public class InputRemoveComponent : IComponent
    {
    }
}
