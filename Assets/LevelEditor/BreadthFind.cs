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

                var neighbours = GetUncheckedNeighbours(currentPosition);

                foreach (var neighbour in neighbours)
                {
                    var found = neighbour.Entities.SingleOrDefault(x => toFind(x));
                    if (found != null)
                    {
                        return found;
                    }

                    if (!neighbour.Entities.Any(x => pathEndIdentifier(x)))
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