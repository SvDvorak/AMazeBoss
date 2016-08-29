using UnityEngine;

namespace Assets.Level.Editor_
{
    public class EditorWorldObject
    {
        public readonly string Type;
        public readonly bool Singleton;
        private readonly string _path;

        public EditorWorldObject(string type, bool singleton, string path)
        {
            Type = type;
            Singleton = singleton;
            _path = path;
        }

        public GameObject LoadView()
        {
            return Resources.Load<GameObject>(_path);
        }

        public GameObject LoadPreview()
        {
            var preview = Resources.Load<GameObject>(_path + "_preview");

            if (preview == null)
            {
                preview = LoadView();
            }

            return preview;
        }
    }
}