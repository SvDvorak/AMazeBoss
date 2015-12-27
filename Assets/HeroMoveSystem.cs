using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class HeroMoveSystem : IExecuteSystem, ISetPool
    {
        private Group _heroes;

        private readonly Dictionary<KeyCode, TilePos> _moveDirections = new Dictionary<KeyCode, TilePos>
            {
                { KeyCode.UpArrow, new TilePos(0, 1) },
                { KeyCode.DownArrow, new TilePos(0, -1) },
                { KeyCode.LeftArrow, new TilePos(-1, 0) },
                { KeyCode.RightArrow, new TilePos(1, 0) }
            };

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _heroes = pool.GetGroup(Matcher.AllOf(Matcher.Hero, Matcher.Position));
        }

        public void Execute()
        {
            var hero = _heroes.GetSingleEntity();
            var inputMoveDirection = new TilePos(0, 0);

            foreach (var moveDirection in _moveDirections)
            {
                if (UnityEngine.Input.GetKeyDown(moveDirection.Key))
                {
                    inputMoveDirection = moveDirection.Value;
                }
            }

            var newMove = inputMoveDirection + hero.position.Value;
            if (_pool.CanMoveTo(newMove))
            {
                hero.ReplacePosition(newMove);
            }
        }
    }
}