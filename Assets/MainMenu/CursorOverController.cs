using Entitas;
using UnityEngine;

public class CursorOverController : MonoBehaviour
{
    private Entity _entity;

    public void SetEntity(Entity entity)
    {
        _entity = entity;
        _entity.Retain(this);
    }

    public void OnMouseEnter()
    {
        _entity.isSelected = true;
    }

    public void OnMouseExit()
    {
        _entity.isSelected = false;
    }

    public void ReleaseEntity()
    {
        _entity.Release(this);
    }
}