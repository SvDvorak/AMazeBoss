public static class ComponentIds {
    public const int Animator = 0;
    public const int Boss = 1;
    public const int Child = 2;
    public const int Destroyed = 3;
    public const int Hero = 4;
    public const int Item = 5;
    public const int Input = 6;
    public const int Paused = 7;
    public const int PlaceableSelected = 8;
    public const int Preview = 9;
    public const int Maintype = 10;
    public const int Moving = 11;
    public const int Parent = 12;
    public const int Position = 13;
    public const int Resource = 14;
    public const int Rotation = 15;
    public const int Subtype = 16;
    public const int Tick = 17;
    public const int Tile = 18;
    public const int TileTemplates = 19;
    public const int View = 20;
    public const int Walkable = 21;

    public const int TotalComponents = 22;

    public static readonly string[] componentNames = {
        "Animator",
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
        "Moving",
        "Parent",
        "Position",
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
        typeof(Assets.HeroComponent),
        typeof(Assets.ItemComponent),
        typeof(Assets.LevelEditor.InputComponent),
        typeof(Assets.LevelEditor.PausedComponent),
        typeof(Assets.LevelEditor.PlaceableSelectedComponent),
        typeof(Assets.LevelEditor.PreviewComponent),
        typeof(Assets.MaintypeComponent),
        typeof(Assets.MovingComponent),
        typeof(Assets.ParentComponent),
        typeof(Assets.PositionComponent),
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