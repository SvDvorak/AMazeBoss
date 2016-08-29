using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Level;
using Entitas;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class ExitGateSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Boss, GameMatcher.Dead).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            GetExitsForCurrentPuzzle().ForEach(x =>
            {
                x.IsBlockingTile(false);
                x.ReplaceExitGate(false);
            });

            _pool.GetHero().isCursed = false;
        }

        public List<Entity> GetExitsForCurrentPuzzle()
        {
            var currentBossConnection = _pool.GetCurrentPuzzleArea().bossConnection;
            return _pool
                .GetEntities(GameMatcher.ExitGate)
                .Where(x => x.bossConnection.BossId == currentBossConnection.BossId)
                .ToList();
        }
    }

    public class LevelExitSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;
        private Group _levelExitsGroup;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _levelExitsGroup = pool.GetGroup(GameMatcher.ExitTrigger);
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var levelExits = _levelExitsGroup.GetEntities();
            if (levelExits.Any(x => x.position.Value == hero.position.Value))
            {
                try
                {
                    // TODO: FIX SO THAT NEXT LEVEL IS LOADED!
                    SceneSetup.LoadScene("Play");
                }
                catch (Exception)
                {
                    SceneManager.LoadScene("gameover");
                }
            }
        }
    }
}
