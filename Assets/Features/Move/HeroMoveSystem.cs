using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class HeroMoveSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.InputMove).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.ActiveTurn; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var moveDirection = hero.inputMove.Direction;
            var newPosition = moveDirection + hero.position.Value;

            var canMoveTo = _pool.OpenTileAt(newPosition);
            var stillInsideSamePuzzle = _pool.IsStillInsideSamePuzzle(hero.position.Value, newPosition);

            if (canMoveTo && !(hero.isSpikesCarried && !stillInsideSamePuzzle))
            {
                hero.ReplacePosition(newPosition);
                hero.ReplaceRotation(LocalDirections.ToRotation(moveDirection));
            }
            else
            {
                var hasKnockedObjectInFront = _pool.KnockObjectsInFront(hero.position.Value, moveDirection, true, 0.4f);
                if (hasKnockedObjectInFront)
                {
                    var knockedObject = _pool.GetEntityAt(newPosition, x => x.hasKnocked);
                    var pushableCanMove = _pool.OpenTileAt(newPosition + moveDirection);
                    if (pushableCanMove && knockedObject.isBox)
                    {
                        hero.IsPushing(true);
                        hero.ReplacePosition(newPosition);
                    }
                    else
                    {
                        hero.HasBumpedIntoObject(true);
                    }

                    hero.ReplaceRotation(LocalDirections.ToRotation(moveDirection));
                }
                else
                {
                }
            }
        }
    }
}