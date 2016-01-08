using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class HeroMoveSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;
        private Group _positionGroup;

        private readonly Dictionary<KeyCode, TilePos> _moveDirections = new Dictionary<KeyCode, TilePos>
            {
                { KeyCode.UpArrow, new TilePos(0, 1) },
                { KeyCode.DownArrow, new TilePos(0, -1) },
                { KeyCode.LeftArrow, new TilePos(-1, 0) },
                { KeyCode.RightArrow, new TilePos(1, 0) }
            };

        private Group _heroGroup;

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _heroGroup = pool.GetGroup(Matcher.Hero);
            _positionGroup = pool.GetGroup(Matcher.Position);
        }

        public void Execute()
        {
            var hero = _heroGroup.GetSingleEntity();
            var inputMoveDirection = GetInputMoveDirection();

            var hasMoved = inputMoveDirection.Length() > 0;
            var newPosition = inputMoveDirection + hero.position.Value;
            var canMove = _pool.CanMoveTo(newPosition) && !hero.isCursed;
            if (hasMoved && canMove)
            {
                if (_positionGroup.GetEntities().Count(x => x.IsMoving()) > 0)
                {
                    hero.ReplaceQueuedPosition(newPosition);
                }
                else
                {
                    hero.ReplacePosition(newPosition);
                }
            }
        }

        private TilePos GetInputMoveDirection()
        {
            var inputMoveDirection = new TilePos(0, 0);

            foreach (var moveDirection in _moveDirections)
            {
                if (UnityEngine.Input.GetKeyDown(moveDirection.Key))
                {
                    inputMoveDirection = moveDirection.Value;
                }
            }
            return inputMoveDirection;
        }
    }
}