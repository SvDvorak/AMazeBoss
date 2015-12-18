using System.Collections.Generic;
using System.Linq;

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
            if (Successful)
            {
                path.RemoveAt(0);
            }
        }

        public bool Successful
        {
            get { return Path.Count > 0; }
        }

        public TilePos NextStep()
        {
            return Path.First();
        }
    }

    public class MovementCalculator
    {
        private readonly List<TilePos> _moveDirections = new List<TilePos>
            {
                new TilePos(0, 1),
                new TilePos(0, -1),
                new TilePos(-1, 0),
                new TilePos(1, 0)
            };

        private List<Tuple<float, List<TilePos>>> _pathsToContinue;
        private HashSet<TilePos> _visited;
        private TilePos _targetPosition;

        public MovementCalculation CalculateMoveToTarget(TilePos currentPosition, TilePos targetPosition)
        {
            _targetPosition = targetPosition;
            var victoryPath = new Tuple<float, List<TilePos>>();

            SetInitialState(currentPosition);
            while (_pathsToContinue.Count > 0)
            {
                var path = PopPathToContinue();
                var pos = GetLastPositionInPath(path.Item2);

                if (pos == _targetPosition)
                {
                    victoryPath = path;
                    break;
                }

                foreach (var moveDirection in _moveDirections)
                {
                    var move = pos + moveDirection;
                    AddNewPathToContinueIfNotBlockedOrVisited(move, path);
                }

                _visited.Add(pos);
                OrderByCost();
            }

            return new MovementCalculation(victoryPath.Item2);
        }

        private void SetInitialState(TilePos currentPosition)
        {
            _visited = new HashSet<TilePos>();
            _pathsToContinue = new List<Tuple<float, List<TilePos>>>();
            AddPathToContinue(0, new List<TilePos> {currentPosition});
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

        private void AddNewPathToContinueIfNotBlockedOrVisited(TilePos move, Tuple<float, List<TilePos>> path)
        {
            if (RoomInfoTwo.Instance.CanMoveTo(move) && !_visited.Contains(move))
            {
                var cost = (_targetPosition - move).Length();
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