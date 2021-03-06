﻿using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.Input
{
    public class HeroInputSystem : IExecuteSystem, ISetPool
    {
        private Group _cameraGroup;

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
            _cameraGroup = pool.GetGroup(Matcher.AllOf(GameMatcher.Camera, GameMatcher.Rotation));
        }

        public void Execute()
        {
            var hero = _pool.GetHero();

            if (hero == null || hero.isCursed || hero.isDead)
            {
                return;
            }

            var inputMoveDirection = GetArrowDirection();
            var hasMoved = inputMoveDirection.Length() > 0;
            Action inputAction = null;
            if (hasMoved)
            {
                if (UnityEngine.Input.GetKey(KeyCode.LeftControl))
                {
                    inputAction = () => hero.ReplaceInputPullItem(inputMoveDirection);
                }
                else
                {
                    inputAction = () => hero.ReplaceInputMove(inputMoveDirection);
                }
            }
            if(UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
            {
                inputAction = () => hero.IsInputItemInteract(true);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                inputAction = () => hero.IsInputCurseSwitch(true);
            }

            if (inputAction != null)
            {
                if (hero.isActiveTurn)
                {
                    inputAction();
                }
                else
                {
                    hero.ReplaceInputQueue(inputAction);
                }
            }
        }

        private TilePos GetArrowDirection()
        {
            var inputMoveDirection = new TilePos(0, 0);

            foreach (var moveDirection in _moveDirections)
            {
                if (UnityEngine.Input.GetKeyDown(moveDirection.Key))
                {
                    inputMoveDirection = moveDirection.Value;
                }
            }

            if (_cameraGroup.count != 0)
            {
                var camera = _cameraGroup.GetSingleEntity();
                inputMoveDirection = inputMoveDirection.Rotate(camera.rotation.Value);
            }

            return inputMoveDirection;
        }
    }
}
