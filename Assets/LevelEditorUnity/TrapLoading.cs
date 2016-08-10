using Assets.LevelEditorUnity;
using UnityEngine;

namespace Assets.Editor
{
    [RequireComponent(typeof(LayoutLink))]
    [ExecuteInEditMode]
    public class TrapLoading : MonoBehaviour
    {
        public bool IsLoaded;

        private MeshRenderer _renderer;
        private LayoutLink _layoutLink;
        private Material _originalMaterial;
        private Material _loadedMaterial;

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
            if (_layoutLink.PuzzleLayout.HasProperty(_layoutLink.Position, "IsLoaded"))
            {
                _layoutLink.PuzzleLayout.PropertySet += PropertyChanged;
                var isNowLoaded =  _layoutLink.PuzzleLayout.GetProperty<bool>(_layoutLink.Position, "IsLoaded");
                UpdateLoadedState(isNowLoaded);
            }
        }

        public void OnDisable()
        {
            _layoutLink.PuzzleLayout.PropertySet -= PropertyChanged;
        }

        private void PropertyChanged(TilePos position, string key, object value)
        {
            if (_layoutLink.Position == position)
            {
                UpdateLoadedState((bool)value);
            }
        }

        private void UpdateLoadedState(bool isNowLoaded)
        {
            IsLoaded = isNowLoaded;
            _renderer.material = IsLoaded ? _loadedMaterial : _originalMaterial;
        }

        //public void OnValidate()
        //{
        //    if (_layoutLink != null && _layoutLink.HasSetLink)
        //    {
        //        SetPropertyIfChanged();
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Layout link not set for " + gameObject.name);
        //    }
        //}

        //private void SetPropertyIfChanged()
        //{
        //    if (_currentIsLoadedValue != IsLoaded)
        //    {
        //        //var setPropertyCommand = new SetPropertyCommand(_layoutLink.PuzzleLayout, _layoutLink.Position, "IsLoaded", IsLoaded);
        //        //PuzzleEditor.CommandsInstance.Execute(setPropertyCommand);
        //    }
        //}
    }
}
