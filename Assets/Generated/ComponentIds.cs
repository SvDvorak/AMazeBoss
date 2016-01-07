public static class ComponentIds {
    public const int ActingTime = 0;
    public const int ActiveTurn = 1;
    public const int Animator = 2;
    public const int Boss = 3;
    public const int Child = 4;
    public const int Destroyed = 5;
    public const int Dynamic = 6;
    public const int Hero = 7;
    public const int Item = 8;
    public const int Input = 9;
    public const int Paused = 10;
    public const int PlaceableSelected = 11;
    public const int Preview = 12;
    public const int Maintype = 13;
    public const int Parent = 14;
    public const int Position = 15;
    public const int QueuedPosition = 16;
    public const int Resource = 17;
    public const int Rotation = 18;
    public const int Subtype = 19;
    public const int Tile = 20;
    public const int TileTemplates = 21;
    public const int View = 22;
    public const int Walkable = 23;

    public const int TotalComponents = 24;

    public static readonly string[] componentNames = {
        "ActingTime",
        "ActiveTurn",
        "Animator",
        "Boss",
        "Child",
        "Destroyed",
        "Dynamic",
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
        "Tile",
        "TileTemplates",
        "View",
        "Walkable"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.ActingTimeComponent),
        typeof(Assets.ActiveTurnComponent),
        typeof(Assets.AnimatorComponent),
        typeof(Assets.BossComponent),
        typeof(Assets.ChildComponent),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.DynamicComponent),
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
        typeof(Assets.TileComponent),
        typeof(Assets.TileTemplates),
        typeof(Assets.ViewComponent),
        typeof(Assets.WalkableComponent)
    };
}