public static class MenuComponentIds {
    public const int Id = 0;
    public const int Resource = 1;
    public const int View = 2;
    public const int ActivateAction = 3;
    public const int MenuItem = 4;
    public const int Selected = 5;
    public const int Text = 6;

    public const int TotalComponents = 7;

    public static readonly string[] componentNames = {
        "Id",
        "Resource",
        "View",
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