using UnityEngine;
using Assets;
using Entitas;
using UnityEngine.UI;

public class MenuItemBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
{
    public Text Text;

    public void OnAttachEntity(Entity entity)
    {
        transform.position = new Vector3(0, entity.id.Value*50);
    }

    public void OnDetachEntity(Entity entity)
    {
    }
}
