using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class ReturnToEditorSystem : IExecuteSystem
    {
        public void Execute()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && PlaySetup.FromEditor)
            {
                SceneManager.LoadScene("Editor");
            }
        }
    }

    public class LevelLoaderSystem : IInitializeSystem, IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Boss, Matcher.Health).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            var levelPath = PlaySetup.LevelPath;

            if (string.IsNullOrEmpty(levelPath))
            {
                levelPath = _pool.levels.Value.First();
                PlaySetup.LevelPath = levelPath;
            }

            FileOperations.FileOperations.Load(levelPath);
        }

        public void Execute(List<Entity> entities)
        {
            var boss = entities.SingleEntity();

            if (boss.health.Value <= 0 && !PlaySetup.FromEditor)
            {
                PlaySetup.LevelPath = GetNext(PlaySetup.LevelPath);
                SceneManager.LoadScene("Play");
            }
        }

        private string GetNext(string path)
        {
            try
            {
                var levels = _pool.levels.Value;
                return levels[levels.IndexOf(path) + 1];
            }
            catch (Exception)
            {
                throw new Exception("Unable to find level after " + path);
            }
        }
    }
}
