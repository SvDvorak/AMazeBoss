public static class ComponentIds {
    public const int Animator = 0;
    public const int Boss = 1;
    public const int Child = 2;
    public const int Destroyed = 3;
    public const int FinishedMoving = 4;
    public const int Hero = 5;
    public const int Item = 6;
    public const int Input = 7;
    public const int Paused = 8;
    public const int PlaceableSelected = 9;
    public const int Preview = 10;
    public const int Maintype = 11;
    public const int Parent = 12;
    public const int Position = 13;
    public const int QueuedPosition = 14;
    public const int Resource = 15;
    public const int Rotation = 16;
    public const int Subtype = 17;
    public const int Tick = 18;
    public const int Tile = 19;
    public const int TileTemplates = 20;
    public const int View = 21;
    public const int Walkable = 22;

    public const int TotalComponents = 23;

    public static readonly string[] componentNames = {
        "Animator",
        "Boss",
        "Child",
        "Destroyed",
        "FinishedMoving",
        "Hero",
        "Item",
        "Input",
        "Paused",
        "PlaceableSelected",
        "Preview",
        "Maintype",
        "Parent",
        "Position",
        "QueuedPosition",
        "Resource",
        "Rotation",
        "Subtype",
        "Tick",
        "Tile",
        "TileTemplates",
        "View",
        "Walkable"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.AnimatorComponent),
        typeof(Assets.BossComponent),
        typeof(Assets.ChildComponent),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.FinishedMoving),
        typeof(Assets.HeroComponent),
        typeof(Assets.ItemComponent),
        typeof(Assets.LevelEditor.InputComponent),
        typeof(Assets.LevelEditor.PausedComponent),
        typeof(Assets.LevelEditor.PlaceableSelectedComponent),
        typeof(Assets.LevelEditor.PreviewComponent),
        typeof(Assets.MaintypeComponent),
        typeof(Assets.ParentComponent),
        typeof(Assets.PositionComponent),
        typeof(Assets.QueuedPositionComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.RotationComponent),
        typeof(Assets.SubtypeComponent),
        typeof(Assets.TickComponent),
        typeof(Assets.TileComponent),
        typeof(Assets.TileTemplates),
        typeof(Assets.ViewComponent),
        typeof(Assets.WalkableComponent)
    };
}