using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class TickSystem : IExecuteSystem, ISetPool, IInitializeSystem
    {
        private Pool _pool;

        public void Initialize()
        {
            _pool.ReplaceTick(5);
        }

        public void Execute()
        {
            _pool.tick.TimeLeft -= Time.deltaTime;

            if (_pool.tick.TimeLeft < 0)
            {
                _pool.ReplaceTick(5);
            }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }
    }
}
