using UnityEngine;

namespace Assets.LevelEditorUnity
{
    public class EditorWorldObject
    {
        public readonly GameObject GameObject;
        public readonly string Type;
        public readonly bool Singleton;

        public EditorWorldObject(string type, bool singleton, GameObject gameObject)
        {
            Type = type;
            GameObject = gameObject;
            Singleton = singleton;
        }
    }
}