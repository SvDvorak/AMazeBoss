using Assets;
using Assets.LevelEditor.Input;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuzzleLayout))]
public class PuzzleEditor : Editor
{
    private TilePos _dragStartPosition;
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

        var tilePos = GetTilePosition(mousePosition);

        switch (uiEvent.type)
        {
            case EventType.Layout:
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                break;
            case EventType.MouseDown:
                if (uiEvent.button == 0)
                {
                    _isDragging = true;
                    _dragStartPosition = tilePos;
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
            Handles.DrawDottedLine(_dragStartPosition.ToV3(), tilePos.ToV3(), 5);
        }

        HandleUtility.Repaint();
    }

    private static TilePos GetTilePosition(Vector2 mousePosition)
    {
        var xzPlane = new Plane(Vector3.up, Vector3.zero);
        var ray = Camera.current.ScreenPointToRay(new Vector2(mousePosition.x, Camera.current.pixelHeight - mousePosition.y));
        float hit;
        if (xzPlane.Raycast(ray, out hit))
        {
            return new TilePos(ray.GetPoint(hit));
        }

        return new TilePos();
    }

    private bool InEditMode
    {
        get { return EditorPrefs.GetBool("PuzzleEditor_InEditMode", false); }
        set { EditorPrefs.SetBool("PuzzleEditor_InEditMode", value); }
    }
}
