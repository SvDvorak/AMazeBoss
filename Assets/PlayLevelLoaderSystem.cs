using System;
using System.Collections.Generic;
using System.Linq;
using Assets.FileOperations;
using Assets.LevelEditor;
using Assets.LevelEditorUnity;
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
                PlaySetup.LevelSave = null;
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
            if (entities.Any(x => x.hasActingSequences))
            {
                return;
            }
            SceneSetup.LoadScene("Play");
        }
    }

    public class GameLevelLoaderSystem : IInitializeSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            var nodes = PuzzleLayout.Instance.Nodes.Values;

            foreach (var node in nodes)
            {
                var newRotation = UnityEngine.Random.Range(0, 4);

                var newObject = _pool.CreateEntity()
                    .ReplacePosition(node.Position)
                    .AddRotation(newRotation);

                WorldObjects.Empty.Do(newObject);
            }

            var hero = _pool.CreateEntity().AddPosition(new TilePos(0, 0));
            WorldObjects.Hero.Do(hero);

            _pool.CreateEntity().AddResource("Camera").AddRotation(0).ReplaceTargetFocusPoint(Vector3.zero);


            _pool.isLevelLoaded = true;
        }
    }

    public class PlayLevelLoaderSystem : IInitializeSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            if (PlaySetup.LevelSave != null)
            {
                LevelLoader.ReadLevelData(PlaySetup.LevelSave, _pool);
            }
            else if (PlaySetup.EditorLevel != null)
            {
                LevelLoader.ReadLevelData(PlaySetup.EditorLevel, _pool);
            }
            else
            {
                LoadBuiltinLevel();
            }

            _pool.isLevelLoaded = true;
        }

        private void LoadBuiltinLevel()
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
            }
            else
            {
                var lastUsedLevelName = PlayerPrefsLevelReader.LastUsedLevelName;

                try
                {
                    PlayerPrefsLevelReader.LoadLevel(lastUsedLevelName);
                }
                catch (LevelLoader.LoadErrorsException ex)
                {
                    Debug.LogWarning(ex.ErrorCount + " errors occurred while trying to read last used level " + lastUsedLevelName);
                }
                catch (Exception)
                {
                    Debug.LogWarning("Unable to read last used level " + lastUsedLevelName);
                }
            }

            _pool.isLevelLoaded = true;
        }
    }
}
