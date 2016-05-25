using Assets;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuzzleLayout))]
public class PuzzleEditor : Editor
{
    private Vector3 _dragStartPosition;
    private bool _isDragging;

    public override void OnInspectorGUI()
    {
        var change = GUILayout.Toggle(InEditMode, "Edit", "Button");
        if (change != InEditMode)
        {
            InEditMode = !InEditMode;
            Debug.Log(InEditMode ? "Started editing" : "Stopped editing");
        }
    }

    public void OnSceneGUI()
    {
        if (!InEditMode)
        {
            return;
        }

        var uiEvent = Event.current;
        var puzzleLayout = (PuzzleLayout)target;
        var mousePosition = uiEvent.mousePosition;

        var dragEndPosition = GetMouseOnXZPlane(mousePosition);

        switch (uiEvent.type)
        {
            case EventType.Layout:
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                break;
            case EventType.MouseDown:
                if (uiEvent.button == 0)
                {
                    _isDragging = true;
                    _dragStartPosition = dragEndPosition;
                }
                break;
            case EventType.MouseUp:
                if (uiEvent.button == 0 && _isDragging)
                {
                    _isDragging = false;
                }
                break;
        }

        if (_isDragging)
        {
            var connectionVector = dragEndPosition - _dragStartPosition;
            var connectionTileVector = Mathf.Abs(connectionVector.x) > Mathf.Abs(connectionVector.z)
                ? new TilePos(connectionVector.x, 0)
                : new TilePos(0, connectionVector.z);
            var tileStart = new TilePos(_dragStartPosition);
            var tileEnd = tileStart + connectionTileVector;
            Handles.DrawDottedLine(tileStart.ToV3(), tileEnd.ToV3(), 5);
        }

        HandleUtility.Repaint();
    }

    private static Vector3 GetMouseOnXZPlane(Vector2 mousePosition)
    {
        var xzPlane = new Plane(Vector3.up, Vector3.zero);
        var ray = Camera.current.ScreenPointToRay(new Vector2(mousePosition.x, Camera.current.pixelHeight - mousePosition.y));
        float hit;
        return xzPlane.Raycast(ray, out hit) ? ray.GetPoint(hit) : new Vector3();
    }

    private bool InEditMode
    {
        get { return EditorPrefs.GetBool("PuzzleEditor_InEditMode", false); }
        set { EditorPrefs.SetBool("PuzzleEditor_InEditMode", value); }
    }
}
