public static class UiComponentIds {
    public const int Id = 0;
    public const int Resource = 1;
    public const int View = 2;
    public const int LevelsInfo = 3;
    public const int ActivateAction = 4;
    public const int MenuItem = 5;
    public const int Selected = 6;
    public const int Text = 7;

    public const int TotalComponents = 8;

    public static readonly string[] componentNames = {
        "Id",
        "Resource",
        "View",
        "LevelsInfo",
        "ActivateAction",
        "MenuItem",
        "Selected",
        "Text"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.IdComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.ViewComponent),
        typeof(Assets.MainMenu.ActivateActionComponent),
        typeof(Assets.MainMenu.MenuItemComponent),
        typeof(Assets.MainMenu.SelectedComponent),
        typeof(Assets.MainMenu.TextComponent)
    };
}