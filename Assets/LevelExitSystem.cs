using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class VictoryExitSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Boss, GameMatcher.Dead).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            GetExitsForCurrentPuzzle().DoForAll(x => x.IsBlockingTile(false));
            _pool.GetHero().isCursed = false;
        }

        public List<Entity> GetExitsForCurrentPuzzle()
        {
            var currentBossConnection = _pool.GetCurrentPuzzleArea().bossConnection;
            return _pool
                .GetEntities(GameMatcher.VictoryExit)
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
                    PlaySetup.LevelPath = GetNext(PlaySetup.LevelPath);
                    SceneSetup.LoadScene("Play");
                }
                catch (Exception)
                {
                    SceneManager.LoadScene("gameover");
                }
            }
        }

        private string GetNext(string path)
        {
            try
            {
                var levels = _pool.levels.Value;
                return levels[levels.IndexOf(path) + 1];
            }
            catch (Exception)
            {
                throw new Exception("Unable to find level after " + path);
            }
        }
    }
}
