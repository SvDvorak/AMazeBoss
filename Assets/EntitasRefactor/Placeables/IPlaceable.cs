using Entitas;

namespace Assets.EntitasRefactor.Placeables
{
    public interface IPlaceable
    {
        Entity Place(Pool pool, TilePos position);
        string Maintype { get; }
    }
}