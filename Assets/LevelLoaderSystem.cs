﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.FileOperations;
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

    public class LevelClearedSystem : IReactiveSystem, ISetPool
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Boss, Matcher.Health).OnEntityAdded(); } }

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var boss = entities.SingleEntity();

            if (boss.health.Value <= 0)
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

    public class LevelRestartSystem : IExecuteSystem
    {
        public void Execute()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Play");
            }
        }
    }

    public class LevelLoaderSystem : IInitializeSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            var levelName = PlaySetup.LevelPath;

            if (string.IsNullOrEmpty(levelName))
            {
                levelName = _pool.levels.Value.First();
                PlaySetup.LevelPath = levelName;
            }

            var level = Resources.Load("Levels/" + levelName) as TextAsset;
            LevelParser.ReadLevelData(level.text);
        }
    }
}
