using Entitas;

namespace Assets.EntitasRefactor.Placeables
{
    public interface IPlaceable
    {
        void Place(Pool pool, TilePos position);
        string Maintype { get; }
    }
}