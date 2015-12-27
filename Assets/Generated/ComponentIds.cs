public static class ComponentIds {
    public const int Boss = 0;
    public const int Child = 1;
    public const int Destroyed = 2;
    public const int Hero = 3;
    public const int Item = 4;
    public const int Input = 5;
    public const int Paused = 6;
    public const int PlaceableSelected = 7;
    public const int Preview = 8;
    public const int Maintype = 9;
    public const int Parent = 10;
    public const int Position = 11;
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
        "Item",
        "Input",
        "Paused",
        "PlaceableSelected",
        "Preview",
        "Maintype",
        "Parent",
        "Position",
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
        typeof(Assets.BossComponent),
        typeof(Assets.ChildComponent),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.HeroComponent),
        typeof(Assets.ItemComponent),
        typeof(Assets.LevelEditor.InputComponent),
        typeof(Assets.LevelEditor.PausedComponent),
        typeof(Assets.LevelEditor.PlaceableSelectedComponent),
        typeof(Assets.LevelEditor.PreviewComponent),
        typeof(Assets.MaintypeComponent),
        typeof(Assets.ParentComponent),
        typeof(Assets.PositionComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.RotationComponent),
        typeof(Assets.SubtypeComponent),
        typeof(Assets.ThinkDelayComponent),
        typeof(Assets.TileComponent),
        typeof(Assets.TileTemplates),
        typeof(Assets.ViewComponent),
        typeof(Assets.WalkableComponent)
    };
}