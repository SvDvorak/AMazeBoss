using Entitas;
using UnityEngine;

namespace Assets
{
    public class AnimationBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
    {
        public Animator Animator;

        public void OnAttachEntity(Entity entity)
        {
            entity.AddAnimator(Animator);
        }

        public void OnDetachEntity(Entity entity)
        {
            entity.RemoveAnimator();
        }
    }
}
