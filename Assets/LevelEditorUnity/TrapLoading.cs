using UnityEngine;

namespace Assets.LevelEditorUnity
{
    [RequireComponent(typeof(LayoutLink))]
    [ExecuteInEditMode]
    public class TrapLoading : MonoBehaviour
    {

        private MeshRenderer _renderer;
        private LayoutLink _layoutLink;
        private Material _originalMaterial;
        private Material _loadedMaterial;
        private PuzzleLayout _layout;

        public void OnEnable()
        {
            _layoutLink = GetComponent<LayoutLink>();
            _renderer = GetComponent<MeshRenderer>();
            _originalMaterial = _renderer.sharedMaterial;
            _loadedMaterial = new Material(_originalMaterial);
            _loadedMaterial.color += new Color(0.5f, 0, 0);
        }

        public void LayoutLinkSet()
        {
            _layout = _layoutLink.PuzzleLayout;
            if (_layout.HasProperty(_layoutLink.Position, "IsLoaded"))
            {
                _layout.PropertySet += PropertyChanged;
                var isNowLoaded =  _layout.GetProperty<bool>(_layoutLink.Position, "IsLoaded");
                UpdateLoadedState(isNowLoaded);
            }
        }

        public void OnDisable()
        {
            if(_layout != null)
            {
                _layout.PropertySet -= PropertyChanged;
            }
        }

        private void PropertyChanged(TilePos position, string key, object value)
        {
            if (_layoutLink.Position == position)
            {
                UpdateLoadedState((bool)value);
            }
        }

        private void UpdateLoadedState(bool isLoaded)
        {
            _renderer.material = isLoaded ? _loadedMaterial : _originalMaterial;
        }
    }
}
