public static class UiComponentIds {
    public const int Child = 0;
    public const int Destroyed = 1;
    public const int EditorOnlyVisual = 2;
    public const int Id = 3;
    public const int InEditor = 4;
    public const int Preview = 5;
    public const int Resource = 6;
    public const int Scene = 7;
    public const int View = 8;
    public const int ActivateAction = 9;
    public const int MenuItem = 10;
    public const int Selected = 11;
    public const int Text = 12;

    public const int TotalComponents = 13;

    public static readonly string[] componentNames = {
        "Child",
        "Destroyed",
        "EditorOnlyVisual",
        "Id",
        "InEditor",
        "Preview",
        "Resource",
        "Scene",
        "View",
        "ActivateAction",
        "MenuItem",
        "Selected",
        "Text"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.ChildComponent),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.IdComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.SceneComponent),
        typeof(Assets.ViewComponent),
        typeof(Assets.MainMenu.ActivateActionComponent),
        typeof(Assets.MainMenu.MenuItemComponent),
        typeof(Assets.MainMenu.SelectedComponent),
        typeof(Assets.MainMenu.TextComponent)
    };
}