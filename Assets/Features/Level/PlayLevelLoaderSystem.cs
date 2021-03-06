﻿using System.Collections.Generic;
using System.Linq;
using Assets.Features.Level;
using Assets.Level;
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
                SceneSetup.LoadScene("Editor");
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
            SceneSetup.LoadScene("Editor");
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

        private class TypeToEntityPerformer
        {
            public readonly string Type;
            public readonly EntityPerformer EntityPerformer;

            public TypeToEntityPerformer(string type, EntityPerformer entityPerformerPerformer)
            {
                Type = type;
                EntityPerformer = entityPerformerPerformer;
            }
        }

        public void Initialize()
        {
	        var placedTiles = new HashSet<TilePos>(_layout.GetObjects("Trap").Select(x => x.Position));

            foreach (var node in _layout.Nodes.Values)
            {
                if (!placedTiles.Contains(node.Position))
                {
                    WorldObjects.Empty.Do(CreateEntity(node.Position), _pool);
                }
            }

            var objectCreator = new List<TypeToEntityPerformer>()
                {
                    new TypeToEntityPerformer("Player", WorldObjects.Hero),
                    new TypeToEntityPerformer("Boss", WorldObjects.Boss),
                    new TypeToEntityPerformer("Trap", WorldObjects.SpikeTrap),
                    new TypeToEntityPerformer("TrapItem", WorldObjects.Spikes),
                    new TypeToEntityPerformer("MoveableBlocker", WorldObjects.Box)
                };

            foreach (var creator in objectCreator)
            {
                var puzzleObjects = _layout.GetObjects(creator.Type);
                foreach (var puzzleObject in puzzleObjects)
                {
                    var entity = creator.EntityPerformer.Do(CreateEntity(puzzleObject.Position), _pool);
					if (puzzleObject.Properties.ContainsKey("Health"))
					{
						entity.ReplaceHealth((int) puzzleObject.Properties["Health"].Value);
					}
					if (puzzleObject.Properties.ContainsKey("IsLoaded") && (bool)puzzleObject.Properties["IsLoaded"].Value)
					{
						entity.ReplaceLoaded(true);
					}
                }
            }

            _pool.CreateEntity().AddResource("Camera").AddRotation(0).ReplaceTargetFocusPoint(Vector3.zero);

            _pool.isLevelLoaded = true;
        }

        private Entity CreateEntity(TilePos position)
        {
            return _pool.CreateEntity().AddPosition(position).AddRotation(0);
        }
    }
}
