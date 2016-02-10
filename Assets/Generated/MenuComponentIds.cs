public static class MenuComponentIds {
    public const int Position = 0;
    public const int Resource = 1;
    public const int View = 2;
    public const int ViewOffset = 3;
    public const int MenuItem = 4;

    public const int TotalComponents = 5;

    public static readonly string[] componentNames = {
        "Position",
        "Resource",
        "View",
        "ViewOffset",
        "MenuItem"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.PositionComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.ViewComponent),
        typeof(Assets.ViewOffsetComponent),
        typeof(Assets.MainMenu.MenuItem)
    };
}