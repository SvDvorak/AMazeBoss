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
    public const int HealthVisual = 9;
    public const int Hero = 10;
    public const int Id = 11;
    public const int Item = 12;
    public const int Input = 13;
    public const int Paused = 14;
    public const int PlaceableSelected = 15;
    public const int Preview = 16;
    public const int Maintype = 17;
    public const int Position = 18;
    public const int QueuedPosition = 19;
    public const int Resource = 20;
    public const int Rotation = 21;
    public const int SpikedTarget = 22;
    public const int SpikesCarried = 23;
    public const int Spikes = 24;
    public const int SpikeTrap = 25;
    public const int Subtype = 26;
    public const int Tile = 27;
    public const int TileTemplates = 28;
    public const int TrapActivated = 29;
    public const int View = 30;
    public const int Walkable = 31;

    public const int TotalComponents = 32;

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
        "HealthVisual",
        "Hero",
        "Id",
        "Item",
        "Input",
        "Paused",
        "PlaceableSelected",
        "Preview",
        "Maintype",
        "Position",
        "QueuedPosition",
        "Resource",
        "Rotation",
        "SpikedTarget",
        "SpikesCarried",
        "Spikes",
        "SpikeTrap",
        "Subtype",
        "Tile",
        "TileTemplates",
        "TrapActivated",
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
        typeof(Assets.HealthVisualComponent),
        typeof(Assets.HeroComponent),
        typeof(Assets.IdComponent),
        typeof(Assets.ItemComponent),
        typeof(Assets.LevelEditor.InputComponent),
        typeof(Assets.LevelEditor.PausedComponent),
        typeof(Assets.LevelEditor.PlaceableSelectedComponent),
        typeof(Assets.LevelEditor.PreviewComponent),
        typeof(Assets.MaintypeComponent),
        typeof(Assets.PositionComponent),
        typeof(Assets.QueuedPositionComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.RotationComponent),
        typeof(Assets.SpikedTargetComponent),
        typeof(Assets.SpikesCarried),
        typeof(Assets.SpikesComponent),
        typeof(Assets.SpikeTrapComponent),
        typeof(Assets.SubtypeComponent),
        typeof(Assets.TileComponent),
        typeof(Assets.TileTemplates),
        typeof(Assets.TrapActivatedComponent),
        typeof(Assets.ViewComponent),
        typeof(Assets.WalkableComponent)
    };
}