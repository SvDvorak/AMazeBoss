using UnityEngine;

namespace Assets.LevelEditorUnity
{
    public class LayoutLink : MonoBehaviour
    {
        private PuzzleLayout _puzzleLayout;
        private string _type;
        private TilePos _position;

        public bool HasSetLink { get; private set; }

        public void SetLinkInfo(PuzzleLayout puzzleLayout, string type, TilePos position)
        {
            HasSetLink = true;
            _puzzleLayout = puzzleLayout;
            _position = position;
            _type = type;

            SendMessage("LayoutLinkSet");
        }

        public void SetProperty(string key, object value)
        {
            _puzzleLayout.SetProperty(_position, key, value);
        }

        public bool GetBoolProperty(string key)
        {
            return _puzzleLayout.GetProperty<bool>(_position, key);
        }

        public bool HasBoolProperty(string key)
        {
            return _puzzleLayout.HasProperty(_position, key);
        }
    }
}
