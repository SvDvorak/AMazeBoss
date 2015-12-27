using Assets;
using Assets.LevelEditor;

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
        typeof(BossComponent),
        typeof(ChildComponent),
        typeof(DestroyedComponent),
        typeof(HeroComponent),
        typeof(InputComponent),
        typeof(ItemComponent),
        typeof(MaintypeComponent),
        typeof(ParentComponent),
        typeof(PausedComponent),
        typeof(PlaceableSelectedComponent),
        typeof(PositionComponent),
        typeof(PreviewComponent),
        typeof(ResourceComponent),
        typeof(RotationComponent),
        typeof(SubtypeComponent),
        typeof(ThinkDelayComponent),
        typeof(TileComponent),
        typeof(TileTemplates),
        typeof(ViewComponent),
        typeof(WalkableComponent)
    };
}