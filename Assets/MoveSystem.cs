using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class MoveSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.View, Matcher.Position).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            //foreach (var positioned in entities)
            //{
            //    var transform = positioned.view.Value.transform;
            //    var newPosition = positioned.position.Value.ToV3();
            //    transform.DOMove(newPosition, 0.5f).SetEase(Ease.Linear);
            //    positioned.ReplaceActingTime(0.5f, () => {});

            //    if (positioned.IsMoving())
            //    {
            //        transform.rotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
            //    }
            //}
        }
    }
}
