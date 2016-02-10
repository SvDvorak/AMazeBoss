public static class MenuComponentIds {
    public const int Id = 0;
    public const int Resource = 1;
    public const int View = 2;
    public const int MenuItem = 3;
    public const int Text = 4;

    public const int TotalComponents = 5;

    public static readonly string[] componentNames = {
        "Id",
        "Resource",
        "View",
        "MenuItem",
        "Text"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.IdComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.ViewComponent),
        typeof(Assets.MainMenu.MenuItemComponent),
        typeof(Assets.MainMenu.TextComponent)
    };
}