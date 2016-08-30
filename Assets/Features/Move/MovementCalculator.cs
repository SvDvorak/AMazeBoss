using System.Collections.Generic;
using System.Linq;
using Assets.Extensions;

namespace Assets
{
    public interface IWalkableValidator
    {
        bool CanMoveTo(TilePos position);
    }

    public class Step
    {
        public TilePos Position { get; private set; }
        public TilePos Direction { get; private set; }
        public bool Turned { get; private set; }

        public Step(TilePos position, TilePos direction, bool turned)
        {
            Position = position;
            Direction = direction;
            Turned = turned;
        }
    }

    public class Path
    {
        public int Cost { get; private set; }
        public List<Step> Steps { get; private set; }

        public Path(TilePos initialPosition, TilePos initialDirection)
        {
            Cost = 0;
            Steps = new List<Step> { new Step(initialPosition, initialDirection, false) };
        }

        protected Path(Path path, int cost, TilePos position, TilePos direction)
        {
            Cost = path.Cost + cost;
            Steps = path.Steps.Concat(new[] { new Step(position, direction, path.GetRotationDifference(direction) > 0) }).ToList();
        }

        public Path Branch(int cost, TilePos position, TilePos direction)
        {
            return new Path(this, cost, position, direction);
        }

        public bool HasStepsLeft { get { return Steps.Count > 0; } }

        public Step NextStep()
        {
            return Steps.First();
        }

        public TilePos GetLastPosition()
        {
            return Steps[Steps.Count - 1].Position;
        }

        public int GetRotationDifference(TilePos direction)
        {
            var lastDirection = Steps[Steps.Count - 1].Direction;
            return lastDirection.RotationDifference(direction);
        }
    }

    public class MovementCalculator
    {
        private readonly IWalkableValidator _walkValidator;

        private List<Path> _pathsToContinue;
        private HashSet<TilePos> _visited;
        private TilePos _targetPosition;

        public MovementCalculator(IWalkableValidator walkValidator)
        {
            _walkValidator = walkValidator;
        }

        public Path CalculateMoveToTarget(
            TilePos initialPosition,
            TilePos initialDirection,
            TilePos targetPosition)
        {
            _targetPosition = targetPosition;
            var victoryPath = new Path(initialPosition, initialDirection);

            SetInitialState(initialPosition, initialDirection);
            while (_pathsToContinue.Count > 0)
            {
                var path = PopPathToContinue();
                var pos = path.GetLastPosition();

                if (pos == _targetPosition)
                {
                    victoryPath = SelectLateTurnPathWithSameCost(path);
                    break;
                }

                foreach (var moveDirection in LocalDirections.GetAll())
                {
                    var newPos = pos + moveDirection;
                    if (_walkValidator.CanMoveTo(newPos) && !_visited.Contains(newPos))
                    {
                        AddNewPathToContinue(newPos, moveDirection, path);
                    }
                }

                _visited.Add(pos);
                OrderByCost();
            }

            victoryPath.Steps.RemoveAt(0);
            return victoryPath;
        }

        private Path SelectLateTurnPathWithSameCost(Path path)
        {
            return _pathsToContinue
                .Where(x => x.Cost == path.Cost && x.GetLastPosition() == path.GetLastPosition())
                .Concat(path)
                .OrderBy(x => GetTurnCosts(x))
                .First();
        }

        private int GetTurnCosts(Path path)
        {
            var cost = 0;
            for (int i = 0; i < path.Steps.Count; i++)
            {
                if (path.Steps[i].Turned)
                {
                    cost += 1/i;
                }
            }
            return cost;
        }

        private void SetInitialState(TilePos initialPosition, TilePos initialDirection)
        {
            _visited = new HashSet<TilePos>();
            _pathsToContinue = new List<Path>();
            _pathsToContinue.Add(new Path(initialPosition, initialDirection));
        }

        private Path PopPathToContinue()
        {
            var path = _pathsToContinue[0];
            _pathsToContinue.RemoveAt(0);
            return path;
        }

        private void AddNewPathToContinue(TilePos pos, TilePos moveDirection, Path path)
        {
            var rotationDifference = path.GetRotationDifference(moveDirection);
            var stepCost = 1 + rotationDifference;
            _pathsToContinue.Add(path.Branch(stepCost, pos, moveDirection));
        }

        private void OrderByCost()
        {
            _pathsToContinue = _pathsToContinue.OrderBy(x => x.Cost).ToList();
        }
    }
}