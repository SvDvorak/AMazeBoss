using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class MovementCalculation
    {
        public readonly List<TilePos> Path;

        public MovementCalculation(List<TilePos> path)
        {
            Path = path;

            RemoveCurrentPositionIfSuccesful(path);
        }

        private void RemoveCurrentPositionIfSuccesful(List<TilePos> path)
        {
            if (HasStepsLeft)
            {
                path.RemoveAt(0);
            }
        }

        public bool HasStepsLeft { get { return Path != null && Path.Count > 0; } }

        public TilePos NextStep()
        {
            return Path.First();
        }
    }

    public interface IWalkableValidator
    {
        bool CanMoveTo(TilePos position);
    }

    public class MovementCalculator
    {
        private readonly List<TilePos> _moveDirections = new List<TilePos>
            {
                new TilePos(0, 1),
                new TilePos(1, 0),
                new TilePos(0, -1),
                new TilePos(-1, 0)
            };

        private readonly IWalkableValidator _walkValidator;

        private List<Tuple<float, List<TilePos>>> _pathsToContinue;
        private HashSet<TilePos> _visited;
        private TilePos _targetPosition;
        private int _startRotation;

        public MovementCalculator(IWalkableValidator walkValidator)
        {
            _walkValidator = walkValidator;
        }

        public MovementCalculation CalculateMoveToTarget(
            TilePos currentPosition,
            int startRotation,
            TilePos targetPosition)
        {
            _targetPosition = targetPosition;
            _startRotation = startRotation;
            var victoryPath = new Tuple<float, List<TilePos>>();

            SetInitialState(currentPosition);
            while (_pathsToContinue.Count > 0)
            {
                var path = PopPathToContinue();
                var pos = GetLastPositionInPath(path.Item2);
                var rotation = GetCurrentRotation(path.Item2);

                if (pos == _targetPosition)
                {
                    victoryPath = path;
                    break;
                }

                foreach (var moveDirection in _moveDirections)
                {
                    var move = pos + moveDirection;
                    var rotationDifference = Mathf.Abs(_directionToRotation[moveDirection] - rotation);
                    AddNewPathToContinueIfNotBlockedOrVisited(move, rotationDifference, path);
                }

                _visited.Add(pos);
                OrderByCost();
            }

            return new MovementCalculation(victoryPath.Item2);
        }

        private int GetCurrentRotation(List<TilePos> path)
        {
            var pathCount = path.Count;
            if (pathCount < 2)
            {
                return _startRotation;
            }

            return _directionToRotation[path[pathCount - 1] - path[pathCount - 2]];
        }

        private readonly Dictionary<TilePos, int> _directionToRotation = new Dictionary<TilePos, int>
            {
                { new TilePos(0, 1), 0 },
                { new TilePos(1, 0), 1 },
                { new TilePos(0, -1), 2 },
                { new TilePos(-1, 0), 3 }
            };

        private void SetInitialState(TilePos currentPosition)
        {
            _visited = new HashSet<TilePos>();
            _pathsToContinue = new List<Tuple<float, List<TilePos>>>();
            AddPathToContinue(0, new List<TilePos> { currentPosition });
        }

        private Tuple<float, List<TilePos>> PopPathToContinue()
        {
            var path = _pathsToContinue[0];
            _pathsToContinue.RemoveAt(0);
            return path;
        }

        private static TilePos GetLastPositionInPath(List<TilePos> path)
        {
            return path[path.Count - 1];
        }

        private void AddNewPathToContinueIfNotBlockedOrVisited(TilePos move, int rotationDifference, Tuple<float, List<TilePos>> path)
        {
            if (_walkValidator.CanMoveTo(move) && !_visited.Contains(move))
            {
                var cost = (_targetPosition - move).ManhattanDistance() + rotationDifference;
                var newPath = path.Item2.Concat(move).ToList();
                AddPathToContinue(cost, newPath);
            }
        }

        private void AddPathToContinue(float cost, List<TilePos> newPath)
        {
            _pathsToContinue.Add(new Tuple<float, List<TilePos>>(cost, newPath));
        }

        private void OrderByCost()
        {
            _pathsToContinue = _pathsToContinue.OrderBy(x => x.Item1).ToList();
        }
    }
}