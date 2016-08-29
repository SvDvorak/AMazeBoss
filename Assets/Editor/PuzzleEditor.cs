using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Editor.Undo;
using Assets.Features.Level;
using Assets.Level;
using Assets.Level.Editor_;
using UnityEngine;
using UnityEditor;

public class PuzzleEditor : EditorWindow
{
    private Vector3 _dragStartPosition;
    private bool _isDragging;
    private bool _isDeleting;
    private static CommandHistory _commandHistory;
    private PuzzleLayoutView _layoutView;

    private int _selectedObjectIndex;
    private bool _updatePreview;
    private TilePos _previousTilePosition;
    private bool _showPropertyDialog;
    private TilePos _propertyDialogPosition;

    [MenuItem("Window/Puzzle Editor")]
    public static void Init()
    {
        var window = GetWindow<PuzzleEditor>();
        window.Show();
    }

    public void OnEnable()
    {
        if (Application.isPlaying)
        {
            return;
        }

        _commandHistory = new CommandHistory(true);
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        LoadLayoutView();
        EditorApplication.hierarchyWindowChanged += LoadLayoutView;
    }

    private void LoadLayoutView()
    {
        var editorObject = GameObject.Find("Editor");
        if (editorObject != null)
        {
            _layoutView = editorObject.GetComponent<PuzzleLayoutView>();
        }
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
        if (Application.isPlaying)
        {
            return;
        }

        var layout = PuzzleLayout.Instance;

        var change = GUILayout.Toggle(InEditMode, "Edit", "Button");
        if (change != InEditMode)
        {
            InEditMode = !InEditMode;
            Tools.hidden = InEditMode;
        }

        var deselect = GUILayout.Button("Deselect placeable");
        if (deselect)
        {
            _selectedObjectIndex = -1;
        }

        var textures = EditorWorldObjects.Instance.GetAllViewObjects().Select(x => (Texture)AssetPreview.GetAssetPreview(x)).ToArray();
        var previouslySelectedIndex = _selectedObjectIndex;
        _selectedObjectIndex = GUILayout.SelectionGrid(_selectedObjectIndex, textures, 3);

        if (_selectedObjectIndex != previouslySelectedIndex)
        {
            _updatePreview = true;
        }

        var shouldClear = GUILayout.Button("Clear");
        if (shouldClear)
        {
            _commandHistory.Execute(new ClearLayoutCommand(layout));
        }
    }

    public void OnSceneGUI(SceneView sceneView)
    {
        if (_layoutView == null)
        {
            return;
        }

        if (!InEditMode)
        {
            _layoutView.RemovePreview();
            return;
        }

        var layout = PuzzleLayout.Instance;
        var uiEvent = Event.current;
        var mousePosition = uiEvent.mousePosition;

        var currentInputPos = GetMouseOnXZPlane(mousePosition);
        var nodeConnection = GetTileAdjustedConnection(_dragStartPosition, currentInputPos);

        EditorWorldObject selectedWorldObject = null;
        if (_selectedObjectIndex >= 0)
        {
            selectedWorldObject = EditorWorldObjects.Instance.GetByIndex(_selectedObjectIndex);
        }

        var inputTilePos = new TilePos(currentInputPos);
        var existingObjectAtPosition = layout.GetObjectAt(inputTilePos);

        if (inputTilePos != _previousTilePosition)
        {
            _updatePreview = true;
        }
        _previousTilePosition = inputTilePos;

        if (_showPropertyDialog)
        {
            var properties = layout.GetObjectAt(_propertyDialogPosition).Properties;
            DrawObjectProperties(properties, layout);
        }

        switch (uiEvent.type)
        {
            case EventType.Layout:
            case EventType.Repaint:
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                break;
            case EventType.MouseDown:
                if (_selectedObjectIndex == 0 && uiEvent.button == 0)
                {
                    _isDragging = true;
                    _dragStartPosition = currentInputPos;
                }
                break;
            case EventType.MouseUp:
                if (uiEvent.button == 0)
                {
                    if (_isDragging)
                    {
                        if (nodeConnection.Length() > 0)
                        {
                            var command = GetNodeConnectionCommand(layout, nodeConnection);
                            _commandHistory.Execute(command);
                        }
                        Repaint();
                        _isDragging = false;
                    }
                    else if (selectedWorldObject == null && existingObjectAtPosition != null)
                    {
                        if (_showPropertyDialog && _propertyDialogPosition == inputTilePos)
                        {
                            _showPropertyDialog = false;
                        }
                        else
                        {
                            _showPropertyDialog = true;
                            _propertyDialogPosition = inputTilePos;
                        }
                    }
                    else if (selectedWorldObject != null && existingObjectAtPosition == null && layout.CanPlaceAt(inputTilePos))
                    {
                        if (selectedWorldObject.Singleton)
                        {
                            _commandHistory.Execute(new SetSingletonObjectCommand(layout, selectedWorldObject.Type, inputTilePos));
                        }
                        else
                        {
                            _commandHistory.Execute(new PlaceNormalObjectCommand(layout, selectedWorldObject.Type, inputTilePos));
                        }
                    }
                }
                else if (uiEvent.button == 1 && existingObjectAtPosition != null)
                {
                    _commandHistory.Execute(new RemoveObjectCommand(layout, inputTilePos));
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
        else if (_updatePreview && selectedWorldObject != null)
        {
            _layoutView.UpdatePreview(selectedWorldObject, new TilePos(currentInputPos));
            _updatePreview = false;
        }

        HandleUtility.Repaint();
    }

    private void DrawObjectProperties(Dictionary<string, string> propertiesObject, PuzzleLayout layout)
    {
        var propertyPosition = Camera.current.WorldToScreenPoint(_propertyDialogPosition.ToV3());

        Handles.BeginGUI();
        GUI.enabled = true;

        GUILayout.BeginArea(new Rect(propertyPosition.x - 100, Screen.height - propertyPosition.y - 100, 220, 200));
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(200));

        if (propertiesObject.Count == 0)
        {
            EditorGUILayout.LabelField("No properties");
        }
        else
        {
            foreach (var property in propertiesObject.ToList())
            {
                var currentValue = bool.Parse(property.Value);
                var possiblyChangedValue = EditorGUILayout.Toggle(property.Key, currentValue);
                if (currentValue != possiblyChangedValue)
                {
                    layout.SetProperty(_propertyDialogPosition, property.Key, possiblyChangedValue);
                }
            }
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();

        Handles.EndGUI();
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