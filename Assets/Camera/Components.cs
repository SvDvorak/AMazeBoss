using Entitas;
using UnityEngine;

namespace Assets
{
    [Game]
    public class CameraComponent : IComponent
    {
        public UnityEngine.Camera Value;
    }

    [Game]
    public class FocusPointComponent : IComponent
    {
        public Vector3 Position;
    }

    [Game]
    public class SavedFocusPointComponent : IComponent
    {
        public Vector3 Position;
    }
}