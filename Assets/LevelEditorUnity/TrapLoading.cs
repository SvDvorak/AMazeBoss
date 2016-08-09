using UnityEngine;

namespace Assets.LevelEditorUnity
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
            if (_layoutLink.HasBoolProperty("IsLoaded"))
            {
                IsLoaded = _layoutLink.GetBoolProperty("IsLoaded");
                UpdateMaterialIfLoaded();
            }
        }

        public void OnValidate()
        {
            if (_layoutLink != null && _layoutLink.HasSetLink)
            {
                UpdateMaterialIfLoaded();
            }
            else
            {
                Debug.LogWarning("Layout link not set for " + gameObject.name);
            }
        }

        private void UpdateMaterialIfLoaded()
        {
            _renderer.material = IsLoaded ? _loadedMaterial : _originalMaterial;
            _layoutLink.SetProperty("IsLoaded", IsLoaded);
        }
    }
}
