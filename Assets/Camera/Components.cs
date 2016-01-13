using Entitas;
using UnityEngine;

namespace Assets
{
    public class CameraComponent : IComponent
    {
        public UnityEngine.Camera Value;
    }

    public class CameraOffsetComponent : IComponent
    {
        public Vector3 Position;
    }
}