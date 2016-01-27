using Entitas;
using UnityEngine;

namespace Assets
{
    public class CameraComponent : IComponent
    {
        public UnityEngine.Camera Value;
    }

    public class FocusPointComponent : IComponent
    {
        public Vector3 Position;
    }

    public class SavedFocusPointComponent : IComponent
    {
        public Vector3 Position;
    }
}