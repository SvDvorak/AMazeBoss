using UnityEngine;
using System.Collections;
using Assets;
using Entitas;
using UnityEngine.UI;

public class HealthBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
{
    public TextMesh TextHealth;

    public void Update()
    {
        TextHealth.transform.LookAt(Camera.main.transform.position);
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
