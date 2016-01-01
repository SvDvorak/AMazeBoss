using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class HeroMoveSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;
        private Group _heroes;
        private Group _positionGroup;

        private readonly Dictionary<KeyCode, TilePos> _moveDirections = new Dictionary<KeyCode, TilePos>
            {
                { KeyCode.UpArrow, new TilePos(0, 1) },
                { KeyCode.DownArrow, new TilePos(0, -1) },
                { KeyCode.LeftArrow, new TilePos(-1, 0) },
                { KeyCode.RightArrow, new TilePos(1, 0) }
            };

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _heroes = pool.GetGroup(Matcher.AllOf(Matcher.Hero, Matcher.Position));
            _positionGroup = pool.GetGroup(Matcher.Position);
        }

        public void Execute()
        {
            var hero = _heroes.GetSingleEntity();
            var inputMoveDirection = GetInputMoveDirection();

            var hasMoved = inputMoveDirection.Length() > 0;
            var newPosition = inputMoveDirection + hero.position.Value;
            var canMoveToTile = _pool.CanMoveTo(newPosition);
            if (hasMoved && canMoveToTile)
            {
                if (_positionGroup.GetEntities().Count(x => x.IsMoving()) > 0)
                {
                    hero.ReplaceQueuedPosition(newPosition);
                    _pool.ReplaceTick(0);
                }
                else
                {
                    hero.ReplacePosition(newPosition);
                    _pool.ReplaceTick(0);
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