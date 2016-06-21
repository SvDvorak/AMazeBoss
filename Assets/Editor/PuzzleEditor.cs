using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Editor.Undo;
using Assets.LevelEditorUnity;
using UnityEngine;
using UnityEditor;

public class PuzzleEditor : EditorWindow
{
    private Vector3 _dragStartPosition;
    private bool _isDragging;
    private bool _isDeleting;
    private CommandHistory _commandHistory;
    private PuzzleLayoutView _layoutView;

    private int _selectedObjectIndex;
    private readonly List<EditorWorldObject> _worldObjects = new List<EditorWorldObject>();

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
        LoadLayoutView();
        EditorApplication.hierarchyWindowChanged += LoadLayoutView;

        _worldObjects.Add(new EditorWorldObject("", Resources.Load<GameObject>("Editor/Node")));
        _worldObjects.Add(new EditorWorldObject("Player", Resources.Load<GameObject>("Editor/Player")));
        _worldObjects.Add(new EditorWorldObject("", Resources.Load<GameObject>("Editor/Boss")));
        _worldObjects.Add(new EditorWorldObject("", Resources.Load<GameObject>("Editor/SpikeTrap")));
    }

    private void LoadLayoutView()
    {
        _layoutView = GameObject.Find("Editor").GetComponent<PuzzleLayoutView>();
    }

    public void OnDisable()
    {
        _layoutView = null;
        EditorApplication.hierarchyWindowChanged -= LoadLayoutView;
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        _commandHistory.Dispose();
        _commandHistory = null;
    }

    public void OnGUI()
    {
        var layout = PuzzleLayout.Instance;

        foreach (var node in PuzzleLayout.Instance.Nodes.Values)
        {
            GUILayout.Label("X: " + node.Position.X + "\t Z: " + node.Position.Z);
        }
        var change = GUILayout.Toggle(InEditMode, "Edit", "Button");
        if (change != InEditMode)
        {
            InEditMode = !InEditMode;
            Tools.hidden = InEditMode;
        }

        var textures = _worldObjects.Select(x => (Texture)AssetPreview.GetAssetPreview(x.GameObject)).ToArray();
        _selectedObjectIndex = GUILayout.SelectionGrid(_selectedObjectIndex, textures, 3);

        var shouldClear = GUILayout.Button("Clear");
        if (shouldClear)
        {
            _commandHistory.Execute(new ClearLayoutCommand(layout));
        }
    }

    public void OnSceneGUI(SceneView sceneView)
    {
        if (!InEditMode)
        {
            return;
        }

        var layout = PuzzleLayout.Instance;
        var uiEvent = Event.current;
        var mousePosition = uiEvent.mousePosition;

        var currentInputTilePos = GetMouseOnXZPlane(mousePosition);
        var nodeConnection = GetTileAdjustedConnection(_dragStartPosition, currentInputTilePos);
        var selectedWorldObject = _worldObjects[_selectedObjectIndex];

        switch (uiEvent.type)
        {
            case EventType.Layout:
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                break;
            case EventType.MouseDown:
                if (_selectedObjectIndex == 0 && uiEvent.button == 0)
                {
                    _isDragging = true;
                    _dragStartPosition = currentInputTilePos;
                }
                break;
            case EventType.MouseUp:
                if (selectedWorldObject.Type == "Player")
                {
                    if (uiEvent.button == 0)
                    {
                        _commandHistory.Execute(new AddPlayerCommand(layout, new TilePos(currentInputTilePos)));
                    }
                    else if (uiEvent.button == 1)
                    {
                        _commandHistory.Execute(new RemovePlayerCommand(layout));
                    }
                }
                else if (uiEvent.button == 0 && _isDragging)
                {
                    if (nodeConnection.Length() > 0)
                    {
                        var command = GetNodeConnectionCommand(layout, nodeConnection);
                        _commandHistory.Execute(command);
                    }
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
            _layoutView.UpdatePreview(nodeConnection);
        }
        else
        {
            _layoutView.UpdatePreview(selectedWorldObject, new TilePos(currentInputTilePos));
        }

        HandleUtility.Repaint();
    }

    private ICommand GetNodeConnectionCommand(PuzzleLayout layout, NodeConnection nodeConnection)
    {
        return _isDeleting
            ? new RemoveNodeConnectionCommand(layout, nodeConnection) as ICommand
            : new AddNodeConnectionCommand(layout, nodeConnection);
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