using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class MoveStyleBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
    {
        public Ease Ease;

        public void OnAttachEntity(Entity entity)
        {
            entity.AddMoveAnimationInfo(Ease, 0);
        }

        public void OnDetachEntity(Entity entity)
        {
            entity.RemoveMoveAnimationInfo();
        }
    }
}