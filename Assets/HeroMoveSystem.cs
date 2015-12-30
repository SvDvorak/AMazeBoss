using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class HeroMoveSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;
        private Group _heroes;
        private Group _movingGroup;

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
            _movingGroup = pool.GetGroup(Matcher.Moving);
        }

        public void Execute()
        {
            if (_movingGroup.count > 0)
            {
                return;
            }

            var hero = _heroes.GetSingleEntity();
            var inputMoveDirection = new TilePos(0, 0);

            foreach (var moveDirection in _moveDirections)
            {
                if (UnityEngine.Input.GetKeyDown(moveDirection.Key))
                {
                    inputMoveDirection = moveDirection.Value;
                }
            }

            var hasMoved = inputMoveDirection.Length() > 0;
            var canMoveToTile = _pool.CanMoveTo(inputMoveDirection + hero.position.Value);
            if (hasMoved && canMoveToTile)
            {
                hero.ReplacePosition(inputMoveDirection + hero.position.Value);
                _pool.ReplaceTick(0);
            }
        }
    }
}