using UnityEngine;
using Assets;
using Entitas;

public class CameraBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
{
    public Camera Camera;

    public void OnAttachEntity(Entity entity)
    {
        entity.AddCamera(Camera);
    }

    public void OnDetachEntity(Entity entity)
    {
        entity.RemoveCamera();
    }
}
