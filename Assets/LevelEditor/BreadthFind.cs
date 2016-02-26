using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class BreadthFind
    {
        private readonly Pool _pool;
        private HashSet<TilePos> _checkedPositions;
        private Queue<TilePos> _uncheckedPositions;

        public BreadthFind(Pool pool)
        {
            _pool = pool;
        }

        public Entity FindAllInConnection(Entity initiator, Func<Entity, bool> toFind, Func<List<Entity>, bool> pathEndIdentifier)
        {
            _checkedPositions = new HashSet<TilePos>();
            _uncheckedPositions = new Queue<TilePos>();
            _uncheckedPositions.Enqueue(initiator.position.Value);

            while (_uncheckedPositions.Any())
            {
                var currentPosition = _uncheckedPositions.Dequeue();
                _checkedPositions.Add(currentPosition);

                var foundTarget = _pool.GetEntitiesAt(currentPosition, x => toFind(x)).SingleOrDefault();
                if (foundTarget != null)
                {
                    return foundTarget;
                }

                foreach (var neighbour in GetUncheckedNeighbours(currentPosition))
                {
                    if (!pathEndIdentifier(neighbour.Entities))
                    {
                        _uncheckedPositions.Enqueue(neighbour.Position);
                    }
                }
            }

            return null;
        }

        private IEnumerable<PositionEntities> GetUncheckedNeighbours(TilePos currentPosition)
        {
            return LocalDirections
                .GetAll()
                .Select(x => x + currentPosition)
                .Where(x => !_checkedPositions.Contains(x))
                .Select(x => new PositionEntities(x, _pool.GetEntitiesAt(x)))
                .Where(x => x.Entities.Any());
        }

        private class PositionEntities
        {
            public readonly TilePos Position;
            public readonly List<Entity> Entities;

            public PositionEntities(TilePos position, List<Entity> entities)
            {
                Entities = entities;
                Position = position;
            }
        }
    }
}