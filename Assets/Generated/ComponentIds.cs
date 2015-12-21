public static class ComponentIds {
    public const int Child = 0;
    public const int Destroyed = 1;
    public const int Input = 2;
    public const int Parent = 3;
    public const int Position = 4;
    public const int Preview = 5;
    public const int Resource = 6;
    public const int Rotation = 7;
    public const int Subtype = 8;
    public const int Tile = 9;
    public const int TileSelect = 10;
    public const int TileTemplates = 11;
    public const int View = 12;

    public const int TotalComponents = 13;

    public static readonly string[] componentNames = {
        "Child",
        "Destroyed",
        "Input",
        "Parent",
        "Position",
        "Preview",
        "Resource",
        "Rotation",
        "Subtype",
        "Tile",
        "TileSelect",
        "TileTemplates",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.EntitasRefactor.ChildComponent),
        typeof(Assets.EntitasRefactor.DestroyedComponent),
        typeof(Assets.EntitasRefactor.InputComponent),
        typeof(Assets.EntitasRefactor.ParentComponent),
        typeof(Assets.EntitasRefactor.PositionComponent),
        typeof(Assets.EntitasRefactor.PreviewComponent),
        typeof(Assets.EntitasRefactor.ResourceComponent),
        typeof(Assets.EntitasRefactor.RotationComponent),
        typeof(Assets.EntitasRefactor.SubtypeComponent),
        typeof(Assets.EntitasRefactor.TileComponent),
        typeof(Assets.EntitasRefactor.TileSelectComponent),
        typeof(Assets.EntitasRefactor.TileTemplates),
        typeof(Assets.EntitasRefactor.ViewComponent)
    };
}