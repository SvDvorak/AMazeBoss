public static class ComponentIds {
    public const int Boss = 0;
    public const int Child = 1;
    public const int Destroyed = 2;
    public const int Hero = 3;
    public const int Input = 4;
    public const int Item = 5;
    public const int Maintype = 6;
    public const int Parent = 7;
    public const int Paused = 8;
    public const int PlaceableSelected = 9;
    public const int Position = 10;
    public const int Preview = 11;
    public const int Resource = 12;
    public const int Rotation = 13;
    public const int Subtype = 14;
    public const int ThinkDelay = 15;
    public const int Tile = 16;
    public const int TileTemplates = 17;
    public const int View = 18;
    public const int Walkable = 19;

    public const int TotalComponents = 20;

    public static readonly string[] componentNames = {
        "Boss",
        "Child",
        "Destroyed",
        "Hero",
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
        "ThinkDelay",
        "Tile",
        "TileTemplates",
        "View",
        "Walkable"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.EntitasRefactor.BossComponent),
        typeof(Assets.EntitasRefactor.ChildComponent),
        typeof(Assets.EntitasRefactor.DestroyedComponent),
        typeof(Assets.EntitasRefactor.HeroComponent),
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
        typeof(Assets.EntitasRefactor.ThinkDelayComponent),
        typeof(Assets.EntitasRefactor.TileComponent),
        typeof(Assets.EntitasRefactor.TileTemplates),
        typeof(Assets.EntitasRefactor.ViewComponent),
        typeof(Assets.EntitasRefactor.WalkableComponent)
    };
}