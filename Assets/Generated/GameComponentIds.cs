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
    public const int Camera = 17;
    public const int Character = 18;
    public const int CurrentFocusPoint = 19;
    public const int Cursed = 20;
    public const int CurseSwitch = 21;
    public const int Dead = 22;
    public const int Dynamic = 23;
    public const int EditorViewMode = 24;
    public const int ExitTrigger = 25;
    public const int GameObject = 26;
    public const int Health = 27;
    public const int HealthVisual = 28;
    public const int Hero = 29;
    public const int Input = 30;
    public const int InputCurseSwitch = 31;
    public const int InputItemInteract = 32;
    public const int InputMove = 33;
    public const int InputPlace = 34;
    public const int InputPullItem = 35;
    public const int InputQueue = 36;
    public const int InputRemove = 37;
    public const int Knocked = 38;
    public const int LevelLoaded = 39;
    public const int Levels = 40;
    public const int Loaded = 41;
    public const int Maintype = 42;
    public const int ObjectPositionCache = 43;
    public const int Paused = 44;
    public const int Position = 45;
    public const int PuzzleArea = 46;
    public const int Rotation = 47;
    public const int SelectedPlaceable = 48;
    public const int SelectedPlaceablesGroup = 49;
    public const int SetCheckpoint = 50;
    public const int SpikesCarried = 51;
    public const int Spikes = 52;
    public const int SpikeTrap = 53;
    public const int Subtype = 54;
    public const int TargetFocusPoint = 55;
    public const int TileTemplates = 56;
    public const int TrapActivated = 57;
    public const int VictoryExit = 58;
    public const int ViewOffset = 59;
    public const int Wall = 60;

    public const int TotalComponents = 61;

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
        "Camera",
        "Character",
        "CurrentFocusPoint",
        "Cursed",
        "CurseSwitch",
        "Dead",
        "Dynamic",
        "EditorViewMode",
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
        "PuzzleArea",
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
        "VictoryExit",
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
        typeof(Assets.Camera.CameraComponent),
        typeof(Assets.CharacterComponent),
        typeof(Assets.Camera.CurrentFocusPointComponent),
        typeof(Assets.CursedComponent),
        typeof(Assets.CurseSwitchComponent),
        typeof(Assets.DeadComponent),
        typeof(Assets.DynamicComponent),
        typeof(Assets.LevelEditor.EditorViewMode),
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
        typeof(Assets.PuzzleAreaComponent),
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
        typeof(Assets.VictoryExitComponent),
        typeof(Assets.ViewOffsetComponent),
        typeof(Assets.WallComponent)
    };
}