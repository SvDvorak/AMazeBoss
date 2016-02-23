using Assets.LevelEditor.Placeables;

namespace Assets.LevelEditor
{
    public static class AllPlaceables
    {
        public static IPlaceable Empty { get { return new Tile(MainTileType.Normal.ToString()); } }
        public static IPlaceable Pillar { get { return new Tile(MainTileType.Pillar.ToString(), e => e.IsBlockingTile(true)); } }
        public static IPlaceable Wall { get { return new Tile(MainTileType.Wall.ToString(), e => e.IsBlockingTile(true)); } }
        public static IPlaceable SpikeTrap { get { return new Tile(MainTileType.SpikeTrap.ToString(), e => e.IsDynamic(true).IsSpikeTrap(true)); } }
        public static IPlaceable CurseTrigger { get { return new Tile(MainTileType.CurseTrigger.ToString(), e => e.IsDynamic(true).IsCurseSwitch(true)); } }
        public static IPlaceable WallTrap { get { return new Item(ItemType.PillarTrap.ToString(), e => e.IsDynamic(true).IsSpikeTrap(true)); } }
        public static IPlaceable Hero { get { return new Item(ItemType.Hero.ToString(), e => e.IsDynamic(true).IsHero(true).AddHealth(3)); } }
        public static IPlaceable Boss { get { return new Item(ItemType.Boss.ToString(), e => e.IsDynamic(true).IsBlockingTile(true).IsBoss(true).IsCursed(true).AddHealth(3)); } }
        public static IPlaceable Spikes { get { return new Spikes(); } }
        public static IPlaceable Box { get { return new Item(ItemType.Box.ToString(), e => e.IsBlockingTile(true).IsDynamic(true).IsBox(true)); } }
        public static IPlaceable VictoryExit { get { return new Item(ItemType.VictoryExit.ToString(), e => e.IsDynamic(true).IsBlockingTile(true).IsVictoryExit(true)); } }
        public static IPlaceable LevelExitTrigger { get { return new Item(ItemType.LevelExitTrigger.ToString(), e => e.IsDynamic(true).IsLevelExitTrigger(true).IsEditorOnlyVisual(true)); } }
    }
}