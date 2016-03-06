public static class GameComponentIds {
    public const int Child = 0;
    public const int Destroyed = 1;
    public const int EditorOnlyVisual = 2;
    public const int Id = 3;
    public const int InEditor = 4;
    public const int Preview = 5;
    public const int Resource = 6;
    public const int Scene = 7;
    public const int View = 8;
    public const int ActingSequences = 9;
    public const int ActiveTurn = 10;
    public const int Animator = 11;
    public const int Attacking = 12;
    public const int BlockingTile = 13;
    public const int Boss = 14;
    public const int BossConnection = 15;
    public const int Box = 16;
    public const int BumpedIntoObject = 17;
    public const int Camera = 18;
    public const int Character = 19;
    public const int CurrentFocusPoint = 20;
    public const int Cursed = 21;
    public const int CurseSwitch = 22;
    public const int Dead = 23;
    public const int Dynamic = 24;
    public const int EditorViewMode = 25;
    public const int ExitGate = 26;
    public const int ExitTrigger = 27;
    public const int GameObject = 28;
    public const int Health = 29;
    public const int HealthVisual = 30;
    public const int Hero = 31;
    public const int Input = 32;
    public const int InputCurseSwitch = 33;
    public const int InputItemInteract = 34;
    public const int InputMove = 35;
    public const int InputPlace = 36;
    public const int InputPullItem = 37;
    public const int InputQueue = 38;
    public const int InputRemove = 39;
    public const int Knocked = 40;
    public const int LevelLoaded = 41;
    public const int Levels = 42;
    public const int Loaded = 43;
    public const int Maintype = 44;
    public const int ObjectPositionCache = 45;
    public const int Paused = 46;
    public const int Position = 47;
    public const int Pulling = 48;
    public const int Pushing = 49;
    public const int PuzzleArea = 50;
    public const int Rocked = 51;
    public const int Rotation = 52;
    public const int SelectedPlaceable = 53;
    public const int SelectedPlaceablesGroup = 54;
    public const int SetCheckpoint = 55;
    public const int SpikesCarried = 56;
    public const int Spikes = 57;
    public const int SpikeTrap = 58;
    public const int Subtype = 59;
    public const int TargetFocusPoint = 60;
    public const int TileTemplates = 61;
    public const int TrapActivated = 62;
    public const int ViewOffset = 63;
    public const int Wall = 64;

    public const int TotalComponents = 65;

    public static readonly string[] componentNames = {
        "Child",
        "Destroyed",
        "EditorOnlyVisual",
        "Id",
        "InEditor",
        "Preview",
        "Resource",
        "Scene",
        "View",
        "ActingSequences",
        "ActiveTurn",
        "Animator",
        "Attacking",
        "BlockingTile",
        "Boss",
        "BossConnection",
        "Box",
        "BumpedIntoObject",
        "Camera",
        "Character",
        "CurrentFocusPoint",
        "Cursed",
        "CurseSwitch",
        "Dead",
        "Dynamic",
        "EditorViewMode",
        "ExitGate",
        "ExitTrigger",
        "GameObject",
        "Health",
        "HealthVisual",
        "Hero",
        "Input",
        "InputCurseSwitch",
        "InputItemInteract",
        "InputMove",
        "InputPlace",
        "InputPullItem",
        "InputQueue",
        "InputRemove",
        "Knocked",
        "LevelLoaded",
        "Levels",
        "Loaded",
        "Maintype",
        "ObjectPositionCache",
        "Paused",
        "Position",
        "Pulling",
        "Pushing",
        "PuzzleArea",
        "Rocked",
        "Rotation",
        "SelectedPlaceable",
        "SelectedPlaceablesGroup",
        "SetCheckpoint",
        "SpikesCarried",
        "Spikes",
        "SpikeTrap",
        "Subtype",
        "TargetFocusPoint",
        "TileTemplates",
        "TrapActivated",
        "ViewOffset",
        "Wall"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Assets.ChildComponent),
        typeof(Assets.DestroyedComponent),
        typeof(Assets.LevelEditor.EditorOnlyVisual),
        typeof(Assets.IdComponent),
        typeof(Assets.LevelEditor.InEditorComponent),
        typeof(Assets.LevelEditor.PreviewComponent),
        typeof(Assets.ResourceComponent),
        typeof(Assets.SceneComponent),
        typeof(Assets.ViewComponent),
        typeof(Assets.ActingSequencesComponent),
        typeof(Assets.ActiveTurnComponent),
        typeof(Assets.AnimatorComponent),
        typeof(Assets.AttackingComponent),
        typeof(Assets.BlockingTileComponent),
        typeof(Assets.BossComponent),
        typeof(Assets.BossConnectionComponent),
        typeof(Assets.BoxComponent),
        typeof(Assets.BumpedIntoObject),
        typeof(Assets.Camera.CameraComponent),
        typeof(Assets.CharacterComponent),
        typeof(Assets.Camera.CurrentFocusPointComponent),
        typeof(Assets.CursedComponent),
        typeof(Assets.CurseSwitchComponent),
        typeof(Assets.DeadComponent),
        typeof(Assets.DynamicComponent),
        typeof(Assets.LevelEditor.EditorViewMode),
        typeof(Assets.ExitGateComponent),
        typeof(Assets.ExitTriggerComponent),
        typeof(Assets.GameObjectComponent),
        typeof(Assets.HealthComponent),
        typeof(Assets.HealthVisualComponent),
        typeof(Assets.HeroComponent),
        typeof(Assets.LevelEditor.InputComponent),
        typeof(Assets.Input.InputCurseSwitchComponent),
        typeof(Assets.Input.InputItemInteractComponent),
        typeof(Assets.Input.InputMoveComponent),
        typeof(Assets.LevelEditor.InputPlaceComponent),
        typeof(Assets.Input.InputPullItemComponent),
        typeof(Assets.Input.InputQueueComponent),
        typeof(Assets.LevelEditor.InputRemoveComponent),
        typeof(Assets.KnockedComponent),
        typeof(Assets.LevelLoaded),
        typeof(Assets.LevelsComponent),
        typeof(Assets.LoadedComponent),
        typeof(Assets.MaintypeComponent),
        typeof(Assets.ObjectPositionCacheComponent),
        typeof(Assets.LevelEditor.PausedComponent),
        typeof(Assets.PositionComponent),
        typeof(Assets.PullingComponent),
        typeof(Assets.PushingComponent),
        typeof(Assets.PuzzleAreaComponent),
        typeof(Assets.RockedComponent),
        typeof(Assets.RotationComponent),
        typeof(Assets.LevelEditor.SelectedPlaceableComponent),
        typeof(Assets.LevelEditor.SelectedPlaceablesGroupComponent),
        typeof(Assets.SetCheckpoint),
        typeof(Assets.SpikesCarriedComponent),
        typeof(Assets.SpikesComponent),
        typeof(Assets.SpikeTrapComponent),
        typeof(Assets.SubtypeComponent),
        typeof(Assets.Camera.TargetFocusPointComponent),
        typeof(Assets.TileTemplates),
        typeof(Assets.TrapActivatedComponent),
        typeof(Assets.ViewOffsetComponent),
        typeof(Assets.WallComponent)
    };
}