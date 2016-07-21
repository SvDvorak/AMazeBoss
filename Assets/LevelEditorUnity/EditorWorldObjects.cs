using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditorUnity;
using UnityEngine;

public class EditorWorldObjects
{
    public static readonly EditorWorldObjects Instance = new EditorWorldObjects();

    private readonly List<EditorWorldObject> _worldObjects = new List<EditorWorldObject>();

    private EditorWorldObjects()
    {
        _worldObjects.Add(new EditorWorldObject("", false, Resources.Load<GameObject>("Editor/Node")));
        _worldObjects.Add(new EditorWorldObject("Player", true, Resources.Load<GameObject>("Editor/Player")));
        _worldObjects.Add(new EditorWorldObject("Boss", true, Resources.Load<GameObject>("Editor/Boss")));
        _worldObjects.Add(new EditorWorldObject("Trap", false, Resources.Load<GameObject>("Editor/SpikeTrap")));
        _worldObjects.Add(new EditorWorldObject("TrapItem", false, Resources.Load<GameObject>("Editor/Spikes")));
        _worldObjects.Add(new EditorWorldObject("MoveableBlocker", false, Resources.Load<GameObject>("Editor/Box")));
    }

    public IEnumerable<GameObject> GetAllGameObjects()
    {
        return _worldObjects.Select(x => x.GameObject);
    }

    public EditorWorldObject GetByIndex(int index)
    {
        return _worldObjects.Count > 0 ? _worldObjects[index] : null;
    }

    public GameObject GetByType(string type)
    {
        return _worldObjects.Single(x => x.Type == type).GameObject;
    }
}