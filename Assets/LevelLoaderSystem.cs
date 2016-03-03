using System;
using System.Collections.Generic;
using System.Linq;
using Assets.FileOperations;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class ReturnToPreviousViewSystem : IExecuteSystem
    {
        public void Execute()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                SceneSetup.LoadPreviousScene();
            }
        }
    }

    public class PlayerRestartSystem : IExecuteSystem
    {
        public void Execute()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                SceneSetup.LoadScene("Play");
            }
        }
    }

    public class LevelRestartSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return GameMatcher.ActingSequences.OnEntityRemoved(); } }
        public IMatcher ensureComponents { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.Dead); } }

        public void Execute(List<Entity> entities)
        {
            SceneSetup.LoadScene("Play");
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
            var levelData = JsonLevelParser.ReadLevelData(level.text);
            LevelLoader.ReadLevelData(levelData, _pool);
            _pool.isLevelLoaded = true;
        }
    }

    public class EditorLevelLoaderSystem : IInitializeSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            if (PlaySetup.EditorLevel != null)
            {
                LevelLoader.ReadLevelData(PlaySetup.EditorLevel, _pool);
                _pool.isLevelLoaded = true;
            }
            else
            {
                var lastUsedLevelName = PlayerPrefsLevelReader.LastUsedLevelName;

                try
                {
                    PlayerPrefsLevelReader.LoadLevel(lastUsedLevelName);
                    _pool.isLevelLoaded = true;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Unable to read last used level " + lastUsedLevelName + ". Reason: " + ex);
                }
            }
        }
    }
}
