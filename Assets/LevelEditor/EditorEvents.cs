using Assets;

public class TileSelected : GameEvent
{
    public TileSelected(StandardTile selectedTile)
    {
        SelectedTile = selectedTile;
    }

    public StandardTile SelectedTile { get; private set; }
}