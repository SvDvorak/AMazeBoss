using System.Collections.Generic;
using Entitas;
using Entitas.Unity.VisualDebugging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Level
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

        public delegate void SceneChanging();
        public static event SceneChanging OnSceneChanging;

        public static void LoadScene(string nextScene)
        {
            CallSceneChanging();
            CurrentScene = nextScene;
            SceneManager.LoadScene(nextScene);
        }

        private static void CallSceneChanging()
        {
            if (OnSceneChanging != null)
            {
                OnSceneChanging();
            }
        }

        public static void LoadPreviousScene()
        {
            if (SceneHistory.Count < 2)
            {
                Debug.LogWarning("No scene to return to");
                return;
            }
            
            CallSceneChanging();
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