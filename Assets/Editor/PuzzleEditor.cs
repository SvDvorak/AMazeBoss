using Assets;
using Assets.LevelEditorUnity;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuzzleLayoutView))]
public class PuzzleEditor : Editor
{
    private Vector3 _dragStartPosition;
    private bool _isDragging;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var change = GUILayout.Toggle(InEditMode, "Edit", "Button");
        if (change != InEditMode)
        {
            InEditMode = !InEditMode;
        }
    }

    public void OnSceneGUI()
    {
        if (!InEditMode)
        {
            return;
        }

        var uiEvent = Event.current;
        var mousePosition = uiEvent.mousePosition;

        var dragEndPosition = GetMouseOnXZPlane(mousePosition);
        var nodeConnection = GetTileAdjustedConnection(_dragStartPosition, dragEndPosition);

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
                if (_isDragging)
                {
                    if (uiEvent.button == 0)
                    {
                        PuzzleLayout.Instance.AddNodeConnection(nodeConnection);
                    }
                    else if (uiEvent.button == 1)
                    {
                        PuzzleLayout.Instance.RemoveNodeConnection(nodeConnection);
                    }
                    _isDragging = false;
                }
                break;
        }

        if (_isDragging)
        {
            Handles.DrawDottedLine(nodeConnection.Start.ToV3(), nodeConnection.End.ToV3(), 5);
        }

        HandleUtility.Repaint();
    }

    private static NodeConnection GetTileAdjustedConnection(Vector3 start, Vector3 end)
    {
        var connectionVector = end - start;
        var connectionTileVector = Mathf.Abs(connectionVector.x) > Mathf.Abs(connectionVector.z)
            ? new TilePos(connectionVector.x, 0)
            : new TilePos(0, connectionVector.z);
        var tileStart = new TilePos(start);
        return new NodeConnection(tileStart, tileStart + connectionTileVector);
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
