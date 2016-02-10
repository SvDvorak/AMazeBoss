using Entitas;
using UnityEngine;

namespace Assets
{
    [Game]
    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    [Game]
    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }
}