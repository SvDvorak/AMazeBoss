using System.Collections.Generic;
using Assets;
using Assets.Editor.Undo;
using Assets.LevelEditorUnity;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PuzzleEditor : EditorWindow
{
    private Vector3 _dragStartPosition;
    private bool _isDragging;
    private bool _isDeleting;
    private CommandHistory _commandHistory;

    [MenuItem("Window/Puzzle Editor")]
    public static void Init()
    {
        var window = GetWindow<PuzzleEditor>();
        window.Show();
    }

    public void OnEnable()
    {
        _commandHistory = new CommandHistory(true);
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    public void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        _commandHistory.Dispose();
        _commandHistory = null;
    }

    public void OnGUI()
    {
        foreach (var node in PuzzleLayout.Instance.Nodes.Values)
        {
            GUILayout.Label("X: " + node.Position.X + "\t Z: " + node.Position.Z);
        }
        var change = GUILayout.Toggle(InEditMode, "Edit", "Button");
        if (change != InEditMode)
        {
            InEditMode = !InEditMode;
            Tools.hidden = InEditMode;

            if (!InEditMode)
            {
                EditorSceneManager.MarkAllScenesDirty();
            }
        }

        var shouldClear = GUILayout.Button("Clear");
        if (shouldClear)
        {
            _commandHistory.Execute(new ClearLayoutCommand());
        }
    }

    public void OnSceneGUI(SceneView sceneView)
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
                if (uiEvent.button == 0 && _isDragging)
                {
                    var command = GetNodeConnectionCommand(nodeConnection);
                    _commandHistory.Execute(command);
                    Repaint();
                    _isDragging = false;
                }
                break;
            case EventType.KeyDown:
                _isDeleting = uiEvent.keyCode == KeyCode.LeftControl;
                break;
            case EventType.KeyUp:
                if (uiEvent.keyCode == KeyCode.LeftControl)
                {
                    _isDeleting = false;
                }
                break;
        }

        if (_isDragging)
        {
            Handles.DrawDottedLine(nodeConnection.Start.ToV3(), nodeConnection.End.ToV3(), 5);
        }

        HandleUtility.Repaint();
    }

    private ICommand GetNodeConnectionCommand(NodeConnection nodeConnection)
    {
        return _isDeleting
            ? new RemoveNodeConnectionCommand(nodeConnection) as ICommand
            : new AddNodeConnectionCommand(nodeConnection);
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