public static class ComponentIds {
    public const int ActingTime = 0;
    public const int ActiveTurn = 1;
    public const int Animator = 2;
    public const int Boss = 3;
    public const int Camera = 4;
    public const int Child = 5;
    public const int Cursed = 6;
    public const int CurseSwitch = 7;
    public const int Destroyed = 8;
    public const int Dynamic = 9;
    public const int FocusPoint = 10;
    public const int Health = 11;
    public const int HealthVisual = 12;
    public const int Hero = 13;
    public const int Id = 14;
    public const int Item = 15;
    public const int Input = 16;
    public const int Paused = 17;
    public const int PlaceableSelected = 18;
    public const int Preview = 19;
    public const int Levels = 20;
    public const int Maintype = 21;
    public const int Position = 22;
    public const int QueuedPosition = 23;
    public const int Resource = 24;
    public const int Rotation = 25;
    public const int SpikedTarget = 26;
    public const int SpikesCarried = 27;
    public const int Spikes = 28;
    public const int SpikeTrap = 29;
    public const int Subtype = 30;
    public const int Tile = 31;
    public const int TileTemplates = 32;
    public const int TrapActivated = 33;
    public const int View = 34;
    public const int Walkable = 35;

    public const int TotalComponents = 36;

    public static readonly string[] componentNames = {
        "ActingTime",
        "ActiveTurn",
        "Animator",
        "Boss",
        "Camera",
        "Child",
        "Cursed",
        "CurseSwitch",
        "Destroyed",
        "Dynamic",
        "FocusPoint",
        "Health",
        "HealthVisual",
        "Hero",
        "Id",
        "Item",
        "Input",
        "Paused",
        "PlaceableSelected",
        "Preview",
        "Levels",
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
        typeof(Assets.CameraComponent),
        typeof(Assets.ChildComponent),
        typeof(Assets.Cursed),
        typeof(Assets.CurseSwitch),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.DynamicComponent),
        typeof(Assets.FocusPointComponent),
        typeof(Assets.HealthComponent),
        typeof(Assets.HealthVisualComponent),
        typeof(Assets.HeroComponent),
        typeof(Assets.IdComponent),
        typeof(Assets.ItemComponent),
        typeof(Assets.LevelEditor.InputComponent),
        typeof(Assets.LevelEditor.PausedComponent),
        typeof(Assets.LevelEditor.PlaceableSelectedComponent),
        typeof(Assets.LevelEditor.PreviewComponent),
        typeof(Assets.Levels),
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
        typeof(Assets.BlockingTileComponent)
    };
}