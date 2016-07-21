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
        private readonly PuzzleLayout _layout;
        private Pool _pool;

        public GameLevelLoaderSystem(PuzzleLayout layout)
        {
            _layout = layout;
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            var placedTiles = new HashSet<TilePos>();

            var trapObjects = _layout.GetObjects("Trap");
            foreach (var trapObject in trapObjects)
            {
                WorldObjects.SpikeTrap.Do(CreateEntity(trapObject), _pool);
                placedTiles.Add(trapObject);
            }

            foreach (var node in _layout.Nodes.Values)
            {
                if (!placedTiles.Contains(node.Position))
                {
                    WorldObjects.Empty.Do(CreateEntity(node.Position), _pool);
                }
            }

            var playerObjects = _layout.GetObjects("Player");
            if(playerObjects.Count > 0)
            {
                WorldObjects.Hero.Do(CreateEntity(playerObjects.Single()), _pool);
            }

            var bossObjects = _layout.GetObjects("Boss");
            if (bossObjects.Count > 0)
            {
                WorldObjects.Boss.Do(CreateEntity(bossObjects.Single()), _pool);
            }

            var trapItemObjects = _layout.GetObjects("TrapItem");
            foreach (var trapItemObject in trapItemObjects)
            {
                WorldObjects.Spikes.Do(CreateEntity(trapItemObject), _pool);
            }

            _pool.CreateEntity().AddResource("Camera").AddRotation(0).ReplaceTargetFocusPoint(Vector3.zero);

            _pool.isLevelLoaded = true;
        }

        private Entity CreateEntity(TilePos position)
        {
            return _pool.CreateEntity().AddPosition(position).AddRotation(0);
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
