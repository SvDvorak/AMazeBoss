public static class ComponentIds {
    public const int ActingTime = 0;
    public const int ActiveTurn = 1;
    public const int Animator = 2;
    public const int Boss = 3;
    public const int Child = 4;
    public const int Cursed = 5;
    public const int Destroyed = 6;
    public const int Dynamic = 7;
    public const int Health = 8;
    public const int Hero = 9;
    public const int Item = 10;
    public const int Input = 11;
    public const int Paused = 12;
    public const int PlaceableSelected = 13;
    public const int Preview = 14;
    public const int Maintype = 15;
    public const int Parent = 16;
    public const int Position = 17;
    public const int QueuedPosition = 18;
    public const int Resource = 19;
    public const int Rotation = 20;
    public const int SpikesCarried = 21;
    public const int Spikes = 22;
    public const int SpikeTrap = 23;
    public const int Subtype = 24;
    public const int Tile = 25;
    public const int TileTemplates = 26;
    public const int View = 27;
    public const int Walkable = 28;

    public const int TotalComponents = 29;

    public static readonly string[] componentNames = {
        "ActingTime",
        "ActiveTurn",
        "Animator",
        "Boss",
        "Child",
        "Cursed",
        "Destroyed",
        "Dynamic",
        "Health",
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
        "SpikesCarried",
        "Spikes",
        "SpikeTrap",
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
        typeof(Assets.Cursed),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.DynamicComponent),
        typeof(Assets.HealthComponent),
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
        typeof(Assets.SpikesCarried),
        typeof(Assets.SpikesComponent),
        typeof(Assets.SpikeTrapComponent),
        typeof(Assets.SubtypeComponent),
        typeof(Assets.TileComponent),
        typeof(Assets.TileTemplates),
        typeof(Assets.ViewComponent),
        typeof(Assets.WalkableComponent)
    };
}