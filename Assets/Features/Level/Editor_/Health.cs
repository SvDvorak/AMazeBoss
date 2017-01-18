using Assets.Features.Level;
using UnityEngine;

namespace Assets.Level.Editor_
{
    [RequireComponent(typeof(LayoutLink))]
    [ExecuteInEditMode]
    public class Health : MonoBehaviour
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
            if (!_layout.HasProperty(_layoutLink.Position, "Health"))
            {
                _layout.SetProperty(_layoutLink.Position, "Health", 3);
            }

            _layout.PropertySet += PropertyChanged;
            var health = (int)_layout.GetObjectAt(_layoutLink.Position).Properties["Health"].Value;
            UpdateLoadedState(health);
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
            if (_layoutLink.Position == position && key == "Health")
            {
                UpdateLoadedState((int)value);
            }
        }

        private void UpdateLoadedState(int health)
        {

        }
    }
}
