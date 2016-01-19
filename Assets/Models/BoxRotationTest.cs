using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoxRotationTest : MonoBehaviour
{
    private readonly float _startHeight = 1 - Mathf.Sin(45*Mathf.Deg2Rad);
    private bool _animationDone = true;

    void Start ()
    {
        Rotate();
    }

    [ContextMenu("Rotate")]
    public void Rotate()
    {
        if (!_animationDone)
        {
            return;
        }

        var time = 2;
        _animationDone = false;
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(90, 0, 0), time, RotateMode.LocalAxisAdd).OnUpdate(UpdatePosition))
            .Join(transform.DOMoveZ(2, time))
            .SetRelative(true)
            .SetEase(Ease.Linear)
            .OnComplete(LOL);
    }

    private void LOL()
    {
        _animationDone = true;
    }

    private void UpdatePosition()
    {
        var angles = transform.rotation.eulerAngles;
        var position = transform.position;
        transform.position = new Vector3(position.x, Mathf.Sin((angles.x%90+45)*Mathf.Deg2Rad) + _startHeight, position.z);
    }
}
