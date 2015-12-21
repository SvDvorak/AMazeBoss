public static class ComponentIds {
    public const int Input = 0;
    public const int Position = 1;
    public const int Preview = 2;
    public const int Resource = 3;
    public const int Tile = 4;
    public const int TileSelect = 5;
    public const int View = 6;

    public const int TotalComponents = 7;

    public static readonly string[] componentNames = {
        "Input",
        "Position",
        "Preview",
        "Resource",
        "Tile",
        "TileSelect",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.EntitasRefactor.InputComponent),
        typeof(Assets.EntitasRefactor.PositionComponent),
        typeof(Assets.EntitasRefactor.PreviewComponent),
        typeof(Assets.EntitasRefactor.ResourceComponent),
        typeof(Assets.EntitasRefactor.TileComponent),
        typeof(Assets.EntitasRefactor.TileSelectComponent),
        typeof(Assets.EntitasRefactor.ViewComponent)
    };
}