using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class BossPuzzleConnectionSetterSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.PuzzleEdge, GameMatcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> puzzleEdges)
        {
            foreach (var puzzleEdge in puzzleEdges)
            {
                var possibleBoss = new BreadthFind(_pool).FindAllInConnection(puzzleEdge, x => x.isBoss, x => x.isPuzzleEdge);
                if (possibleBoss != null)
                {
                    puzzleEdge.ReplaceBossConnection(possibleBoss.id.Value);
                }
            }
        }

    }
    public class BreadthFind
    {
        private readonly Pool _pool;
        private HashSet<TilePos> _checkedPositions;
        private Queue<TilePos> _uncheckedPositions;

        public BreadthFind(Pool pool)
        {
            _pool = pool;
        }

        public Entity FindAllInConnection(Entity initiator, Func<Entity, bool> toFind, Func<Entity, bool> pathEndIdentifier)
        {
            _checkedPositions = new HashSet<TilePos>();
            _uncheckedPositions = new Queue<TilePos>();
            _uncheckedPositions.Enqueue(initiator.position.Value);

            while (_uncheckedPositions.Any())
            {
                var currentPosition = _uncheckedPositions.Dequeue();
                _checkedPositions.Add(currentPosition);
                Debug.DrawLine(currentPosition.ToV3(), currentPosition.ToV3() + Vector3.up, Color.red);

                var tilePoses = LocalDirections
                    .GetAll()
                    .Select(x => x + currentPosition)
                    .Where(x => !_checkedPositions.Contains(x));
                var neighbours = tilePoses
                    .Select(x => new { pos = x, entities = _pool.GetEntitiesAtPosition(x) })
                    .Where(x => x.entities.Any());

                foreach (var neighbour in neighbours)
                {
                    var found = neighbour.entities.SingleOrDefault(x => toFind(x));
                    if (found != null)
                    {
                        return found;
                    }

                    if (!neighbour.entities.Any(x => pathEndIdentifier(x)))
                    {
                        _uncheckedPositions.Enqueue(neighbour.pos);
                    }
                }
            }

            return null;
        }
    }
}
