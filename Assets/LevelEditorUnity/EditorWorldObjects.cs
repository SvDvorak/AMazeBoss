using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditorUnity;
using UnityEngine;

public class EditorWorldObjects
{
    public static readonly EditorWorldObjects Instance = new EditorWorldObjects();

    private readonly List<EditorWorldObject> _worldObjects;

    private EditorWorldObjects()
    {
        _worldObjects = new List<EditorWorldObject>()
            {
                new EditorWorldObject("", false, "Editor/Node"),
                new EditorWorldObject("Player", true, "Editor/Player"),
                new EditorWorldObject("Boss", true, "Editor/Boss"),
                new EditorWorldObject("Trap", false, "Editor/SpikeTrap"),
                new EditorWorldObject("TrapItem", false, "Editor/Spikes"),
                new EditorWorldObject("MoveableBlocker", false, "Editor/Box")
            };
    }

    public IEnumerable<GameObject> GetAllViewObjects()
    {
        return _worldObjects.Select(x => x.LoadView());
    }

    public EditorWorldObject GetByIndex(int index)
    {
        return _worldObjects.Count > 0 ? _worldObjects[index] : null;
    }

    public GameObject GetByType(string type)
    {
        return _worldObjects.Single(x => x.Type == type).LoadView();
    }
}