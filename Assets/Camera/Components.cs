using Entitas;
using UnityEngine;

namespace Assets.Camera
{
    [Game]
    public class CameraComponent : IComponent
    {
        public UnityEngine.Camera Value;
    }

    [Game]
    public class CurrentFocusPointComponent : IComponent
    {
        public Vector3 Position;
    }

    [Game]
    public class TargetFocusPointComponent : IComponent
    {
        public Vector3 Position;
    }
}