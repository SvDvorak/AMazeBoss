using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class RotateView : MonoBehaviour
{
    private readonly List<Vector3> _mouseDownPositions = new List<Vector3>();
    private LineRenderer _lineRenderer;
    private int _rotation;

    public void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var last = Input.mousePosition;
            _mouseDownPositions.Add(last);

            var first = _mouseDownPositions[0];
            var middle = _mouseDownPositions[_mouseDownPositions.Count / 2];
            var toMiddle = (middle - first);
            var toLast = (last - first);
            var c = toLast.magnitude;
            var a = toMiddle.magnitude;
            var b = (last - middle).magnitude;
            var cAngle = Mathf.Acos((a * a + b * b - c * c) / (2 * a * b));
            var angleInDegrees = Mathf.Rad2Deg * cAngle;

            if (Mathf.Abs(angleInDegrees - 90) < 20)
            {
                var rotDirection = Vector3.Cross(toLast, toMiddle).z < 0 ? -1 : 1;
                _rotation = (_rotation + rotDirection) % 4;
                RotateCamera();
                _mouseDownPositions.Clear();
            }
        }
        else
        {
            if (_mouseDownPositions.Count > 0)
            {
                _mouseDownPositions.Clear();
            }
        }

        _lineRenderer.SetVertexCount(_mouseDownPositions.Count);
        _lineRenderer.SetPositions(_mouseDownPositions.Select(x =>
            {
                var ray = Camera.main.ScreenPointToRay(x);
                return ray.origin + ray.direction;
            }).ToArray());
    }

    private void RotateCamera()
    {
        var cameraTransform = Camera.main.transform;
        var currentRotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);

        DOTween.Sequence()
            .Append(DOTween.To(() => currentRotation, x => currentRotation = x, new Vector3(0, (35 + 90 * _rotation) % 360, 0), 1))
            .OnUpdate(() => UpdateTransform(cameraTransform, Vector3.zero, currentRotation));
        //cameraTransform.DOMove(Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y + 90, Vector3.up) * Vector3.back * 10 + new Vector3(0, 15, 0), 0.5f);
        //cameraTransform.DOLookAt(Vector3.zero, 0.5f);
    }

    private static void UpdateTransform(Transform transform, Vector3 focusPoint, Quaternion spin)
    {
        transform.position = focusPoint + spin * Vector3.back * 10 + new Vector3(0, 15, 0);
        transform.LookAt(focusPoint);
    }
}