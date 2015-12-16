using Assets;

public class TileSelected : GameEvent
{
    public TileSelected(TileType selectedType)
    {
        TileTypeSelected = selectedType;
    }

    public TileType TileTypeSelected { get; private set; }
}