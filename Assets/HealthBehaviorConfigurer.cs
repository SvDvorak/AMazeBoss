using UnityEngine;
using Assets;
using Entitas;

public class HealthBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
{
    public TextMesh TextHealth;

    public void Update()
    {
        TextHealth.transform.LookAt(Pools.pool.GetCamera().transform.position);
    }

    public void OnAttachEntity(Entity entity)
    {
        entity.AddHealthVisual(TextHealth);
    }

    public void OnDetachEntity(Entity entity)
    {
        entity.RemoveHealthVisual();
    }
}
