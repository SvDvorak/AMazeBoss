using Entitas;
using UnityEngine;

namespace Assets
{
    [Game, Ui]
    public class ResourceComponent : IComponent
    {
        public string Path;
    }

    [Game, Ui]
    public class ViewComponent : IComponent
    {
        public GameObject Value;
    }

    [Game]
    public class ViewOffsetComponent : IComponent
    {
        public Vector3 Value;
    }
}