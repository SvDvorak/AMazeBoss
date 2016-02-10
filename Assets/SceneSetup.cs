using Entitas;
using Entitas.Unity.VisualDebugging;

namespace Assets
{
    public static class SceneSetup
    {
        public static Systems CreateSystem()
        {
#if (UNITY_EDITOR)
            return new DebugSystems();
#else
            return new Systems();
#endif
        }
    }
}