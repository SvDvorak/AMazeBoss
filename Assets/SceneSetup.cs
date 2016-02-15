using System.Collections.Generic;
using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public static void LoadScene(string nextScene)
        {
            CurrentScene = nextScene;
            SceneManager.LoadScene(nextScene);
        }

        public static void LoadPreviousScene()
        {
            if (SceneHistory.Count < 2)
            {
                Debug.LogWarning("No scene to return to");
                return;
            }
            
            SceneHistory.Pop();
            SceneManager.LoadScene(CurrentScene);
        }

        public static string CurrentScene
        {
            get { return SceneHistory.Peek(); }
            set
            {
                if (SceneHistory.Count == 0 || CurrentScene != value)
                {
                    SceneHistory.Push(value);
                }
            }
        }

        private static readonly Stack<string> SceneHistory = new Stack<string>(); 
    }
}