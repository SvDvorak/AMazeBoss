using Entitas;
using UnityEngine;

namespace Assets
{
    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }
}