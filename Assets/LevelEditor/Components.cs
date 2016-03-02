using Entitas;
using Entitas.CodeGenerator;

namespace Assets.LevelEditor
{
    [SingleEntity, Game]
    public class PausedComponent : IComponent
    {
    }

    [SingleEntity, Game, Ui]
    public class InEditorComponent : IComponent
    {
    }

    [SingleEntity, Game]
    public class EditorViewMode : IComponent
    {
        public ViewMode Value;
    }

    [Game, Ui]
    public class EditorOnlyVisual : IComponent
    {
        public ViewMode ShowInMode;
    }

    public enum ViewMode
    {
        Normal,
        Area
    }

    [Game]
    public class SelectedPlaceableComponent : IComponent
    {
        public EntityPerformer Value;
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

    [Game, Ui]
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
