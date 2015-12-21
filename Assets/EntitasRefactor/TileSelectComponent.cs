using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    public class TileSelectComponent : IComponent
    {
        public MainTileType Type;
    }

    public class InputComponent : IComponent
    {
    }

    [SingleEntity]
    public class PreviewComponent : IComponent
    {
    }

    public class PositionComponent : IComponent
    {
        public TilePos Value;
    }

    public class TileComponent : IComponent
    {
        public MainTileType Type;
    }

    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }

    public class DestroyedComponent : IComponent
    {
    }
}