using UnityEngine;
using Assets;
using Entitas;
using UnityEngine.UI;

public class MenuItemBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
{
    public void OnAttachEntity(Entity entity)
    {
        var textComponent = GetComponent<Text>();
        textComponent.text = entity.menuItem.Text;
        transform.position = new Vector3(0, -entity.id.Value*50);
    }

    public void OnDetachEntity(Entity entity)
    {
    }
}
