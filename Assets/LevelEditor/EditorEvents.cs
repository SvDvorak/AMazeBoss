using Assets;

public class TileSelected : GameEvent
{
    public TileSelected(Tile selectedTile)
    {
        SelectedTile = selectedTile;
    }

    public Tile SelectedTile { get; private set; }
}