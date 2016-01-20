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
                var movesMade = boss.moveHistory.Value.Count;
                if (movesMade <= 2)
                {
                    continue;
                }

                const int movesToSprint = 3;
                var lastMoves = boss.moveHistory.Value.GetRange(movesMade - movesToSprint, movesToSprint);
                var moveDirection = new TilePos(lastMoves.Sum(pos => pos.X), lastMoves.Sum(pos => pos.Z));
                var currentPosition = boss.position.Value;
                var isSprinting = moveDirection.X == currentPosition.X*movesToSprint || moveDirection.Z == currentPosition.Z*movesToSprint;
                boss.IsBossSprinting(isSprinting);
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
