using Entitas;
using UnityEngine;

namespace Assets
{
    [Game, Menu]
    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    [Game, Menu]
    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }

    [Game, Menu]
    public class ViewOffsetComponent : IComponent
    {
        public Vector3 Value;
    }
}