public static class ComponentIds {
    public const int Child = 0;
    public const int Destroyed = 1;
    public const int Input = 2;
    public const int Item = 3;
    public const int Maintype = 4;
    public const int Parent = 5;
    public const int Paused = 6;
    public const int PlaceableSelected = 7;
    public const int Position = 8;
    public const int Preview = 9;
    public const int Resource = 10;
    public const int Rotation = 11;
    public const int Subtype = 12;
    public const int Tile = 13;
    public const int TileTemplates = 14;
    public const int View = 15;

    public const int TotalComponents = 16;

    public static readonly string[] componentNames = {
        "Child",
        "Destroyed",
        "Input",
        "Item",
        "Maintype",
        "Parent",
        "Paused",
        "PlaceableSelected",
        "Position",
        "Preview",
        "Resource",
        "Rotation",
        "Subtype",
        "Tile",
        "TileTemplates",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.EntitasRefactor.ChildComponent),
        typeof(Assets.EntitasRefactor.DestroyedComponent),
        typeof(Assets.EntitasRefactor.InputComponent),
        typeof(Assets.EntitasRefactor.ItemComponent),
        typeof(Assets.EntitasRefactor.MaintypeComponent),
        typeof(Assets.EntitasRefactor.ParentComponent),
        typeof(Assets.EntitasRefactor.PausedComponent),
        typeof(Assets.EntitasRefactor.PlaceableSelectedComponent),
        typeof(Assets.EntitasRefactor.PositionComponent),
        typeof(Assets.EntitasRefactor.PreviewComponent),
        typeof(Assets.EntitasRefactor.ResourceComponent),
        typeof(Assets.EntitasRefactor.RotationComponent),
        typeof(Assets.EntitasRefactor.SubtypeComponent),
        typeof(Assets.EntitasRefactor.TileComponent),
        typeof(Assets.EntitasRefactor.TileTemplates),
        typeof(Assets.EntitasRefactor.ViewComponent)
    };
}