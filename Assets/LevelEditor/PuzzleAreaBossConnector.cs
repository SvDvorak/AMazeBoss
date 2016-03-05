using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor
{
    public class PuzzleAreaBossConnector
    {
        private readonly Pool _pool;
        private HashSet<TilePos> _checkedPositions;
        private Queue<TilePos> _uncheckedPositions;

        public PuzzleAreaBossConnector(Pool pool)
        {
            _pool = pool;
        }

        public void ConnectWholeAreaToClosestBoss(List<TilePos> startPositions)
        {
            _checkedPositions = new HashSet<TilePos>();
            _uncheckedPositions = new Queue<TilePos>(startPositions);
            Entity closestBoss = null;

            while (_uncheckedPositions.Any())
            {
                var currentPosition = _uncheckedPositions.Dequeue();
                _checkedPositions.Add(currentPosition);

                if (closestBoss == null)
                {
                    var foundTarget = _pool.GetEntitiesAt(currentPosition, x => x.isBoss).SingleOrDefault();
                    if (foundTarget != null)
                    {
                        closestBoss = foundTarget;
                    }
                }

                foreach (var neighbour in GetUncheckedNeighbours(currentPosition))
                {
                    if (neighbour.Entities.Any(x => x.isPuzzleArea))
                    {
                        _uncheckedPositions.Enqueue(neighbour.Position);
                    }
                }
            }

            _checkedPositions
                    .Select(pos => _pool.GetEntityAt(pos, entity => entity.isPuzzleArea))
                    .Where(x => x != null)
                    .ForEach(x => UpdateOrRemoveBossConnection(x, closestBoss));
        }

        private void UpdateOrRemoveBossConnection(Entity puzzleArea, Entity closestBoss)
        {
            if (closestBoss != null)
            {
                puzzleArea.ReplaceBossConnection(closestBoss.id.Value);
            }
            else if(puzzleArea.hasBossConnection)
            {
                puzzleArea.RemoveBossConnection();
            }
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