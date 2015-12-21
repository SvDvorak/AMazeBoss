public static class ComponentIds {
    public const int Destroyed = 0;
    public const int Input = 1;
    public const int Position = 2;
    public const int Preview = 3;
    public const int Resource = 4;
    public const int Tile = 5;
    public const int TileSelect = 6;
    public const int View = 7;

    public const int TotalComponents = 8;

    public static readonly string[] componentNames = {
        "Destroyed",
        "Input",
        "Position",
        "Preview",
        "Resource",
        "Tile",
        "TileSelect",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.EntitasRefactor.DestroyedComponent),
        typeof(Assets.EntitasRefactor.InputComponent),
        typeof(Assets.EntitasRefactor.PositionComponent),
        typeof(Assets.EntitasRefactor.PreviewComponent),
        typeof(Assets.EntitasRefactor.ResourceComponent),
        typeof(Assets.EntitasRefactor.TileComponent),
        typeof(Assets.EntitasRefactor.TileSelectComponent),
        typeof(Assets.EntitasRefactor.ViewComponent)
    };
}