using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class BossSprintSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Boss, Matcher.MoveHistory).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var boss in entities)
            {
                var moveHistory = boss.moveHistory.Value;
                var movesMade = moveHistory.Count;
                if (movesMade <= 2)
                {
                    boss.ReplaceMovesInARow(0);
                    continue;
                }

                var currentMovesInARow = boss.movesInARow.Moves;
                var lastMoves = moveHistory.GetRange(movesMade - currentMovesInARow - 2, currentMovesInARow + 1);
                var moveDirection = new TilePos(lastMoves.Sum(pos => pos.X), lastMoves.Sum(pos => pos.Z));
                var currentPosition = boss.position.Value;
                var isMovingInARow = moveDirection.X == currentPosition.X * currentMovesInARow || moveDirection.Z == currentPosition.Z * currentMovesInARow;
                boss.ReplaceMovesInARow(currentMovesInARow + 1);
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
