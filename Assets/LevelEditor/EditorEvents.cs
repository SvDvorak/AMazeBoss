using Assets;

public class TileSelected : GameEvent
{
    public TileSelected(TileType selected)
    {
        TileTypeSelected = selected;
    }

    public TileType TileTypeSelected { get; private set; }
}