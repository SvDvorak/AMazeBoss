using UnityEngine;
using Assets;
using Entitas;
using UnityEngine.UI;

public class MenuItemBehaviorConfigurer : MonoBehaviour, IGameObjectConfigurer
{
    public float MenuStartHeight;
    private CursorOverController _cursorOverController;

    public void OnAttachEntity(Entity entity)
    {
        var textComponent = GetComponent<Text>();
        textComponent.text = entity.menuItem.Text;
        transform.position = new Vector3(0, -entity.id.Value*50 + MenuStartHeight);

        _cursorOverController = GetComponent<CursorOverController>();
        _cursorOverController.SetEntity(entity);
    }

    public void OnDetachEntity(Entity entity)
    {
        _cursorOverController.ReleaseEntity();
    }
}
