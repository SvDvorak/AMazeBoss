using Assets;

public class TileSelected : GameEvent
{
    public TileSelected(MainTileType selectedType)
    {
        TileTypeSelected = selectedType;
    }

    public MainTileType TileTypeSelected { get; private set; }
}