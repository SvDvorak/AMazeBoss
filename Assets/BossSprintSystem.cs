using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class BossSprintSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Boss, Matcher.MoveHistory).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var boss in entities)
            {
                var moveHistory = boss.moveHistory.Value;
                var movesMade = moveHistory.Count;
                if (movesMade < 2)
                {
                    boss.ReplaceMovesInARow(0);
                    continue;
                }

                var currentMovesInARow = boss.movesInARow.Moves;
                var lastMoves = moveHistory.GetRange(movesMade - (currentMovesInARow + 2), currentMovesInARow + 2);
                var currentPosition = boss.position.Value;
                var allXSame = lastMoves.All(pos => pos.X == currentPosition.X);
                var allZSame = lastMoves.All(pos => pos.Z == currentPosition.Z);
                boss.ReplaceMovesInARow(allXSame || allZSame ? currentMovesInARow + 1 : 1);

                if(boss.movesInARow.Moves > 1)
                {
                    var moveDirection = lastMoves[lastMoves.Count - 1] - lastMoves[lastMoves.Count - 2];

                    var itemInFront = _pool.GetItemAt(currentPosition + moveDirection);
                    if (itemInFront != null && itemInFront.isBlockingTile)
                    {
                        itemInFront.ReplaceKnocked(moveDirection);
                        boss.ReplaceMovesInARow(0);
                    }
                }
            }
        }
    }

    public class MoveHistorySystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.Position.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                var moveHistory = e.hasMoveHistory ? e.moveHistory.Value : new List<TilePos>();
                moveHistory.Add(e.position.Value);
                e.ReplaceMoveHistory(moveHistory);
            }
        }
    }
}
