using Assets.Level.Editor_;
using UnityEditor;
using UnityEngine;

namespace Assets.Features.Level.Editor_
{
    [RequireComponent(typeof(LayoutLink))]
    [ExecuteInEditMode]
    public class HealthView : MonoBehaviour
    {
        private LayoutLink _layoutLink;
        private PuzzleLayout _layout;
        private TextMesh _text;

        public void Awake()
        {
            _layoutLink = GetComponent<LayoutLink>();
            var loadedVisual = Resources.Load<GameObject>("Editor/HealthVisual");
            var textObject = (GameObject)PrefabUtility.InstantiatePrefab(loadedVisual);
            textObject.transform.SetParent(transform, false);
            _text = textObject.GetComponent<TextMesh>();
        }

        public void LayoutLinkSet()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            _layout = _layoutLink.PuzzleLayout;
            if (!_layout.HasProperty(_layoutLink.Position, "Health"))
            {
                _layout.SetProperty(_layoutLink.Position, "Health", 1);
            }

            _layout.PropertySet += PropertyChanged;
            var health = (int)_layout.GetProperty(_layoutLink.Position, "Health").Value;
            SetHealth(health);
        }

        private void SetHealth(int health)
        {
            _text.text = health.ToString();
        }

        public void OnDisable()
        {
            if (_layout != null)
            {
                _layout.PropertySet -= PropertyChanged;
            }
        }

        private void PropertyChanged(TilePos position, string key, object value)
        {
            if (_layoutLink.Position == position)
            {
                SetHealth((int)value);
            }
        }
    }
}
