using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor.Placeables;
using Entitas;
using Entitas.CodeGenerator;

namespace Assets.LevelEditor
{
    [SingleEntity, Game]
    public class PausedComponent : IComponent
    {
    }

    [Game]
    public class SelectedPlaceableComponent : IComponent
    {
        public IPlaceable Value;
    }

    [SingleEntity, Game]
    public class SelectedPlaceablesGroupComponent : IComponent
    {
        public SelectionGroup Group;
    }

    [SingleEntity, Game]
    public class InputComponent : IComponent
    {
    }

    [SingleEntity, Game]
    public class PreviewComponent : IComponent
    {
    }

    [SingleEntity, Game]
    public class InputPlaceComponent : IComponent
    {
    }

    [SingleEntity, Game]
    public class InputRemoveComponent : IComponent
    {
    }
}
