public static class UiComponentIds {
    public const int Child = 0;
    public const int Destroyed = 1;
    public const int Id = 2;
    public const int Resource = 3;
    public const int Scene = 4;
    public const int View = 5;
    public const int ActivateAction = 6;
    public const int MenuItem = 7;
    public const int Selected = 8;
    public const int Text = 9;

    public const int TotalComponents = 10;

    public static readonly string[] componentNames = {
        "Child",
        "Destroyed",
        "Id",
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