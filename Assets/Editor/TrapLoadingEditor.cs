using Assets.LevelEditorUnity;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(LayoutLink))]
    public class LayoutLinkEditor : UnityEditor.Editor
    {
        public void OnEnable()
        {
        }

        public void OnSceneGUI()
        {
            var layoutLink = (LayoutLink)target;

            var uiEvent = Event.current;
            var mousePosition = uiEvent.mousePosition;

            //var xzPlane = new Plane(Vector3.up, Vector3.zero);
            //var ray = UnityEngine.Camera.current.ScreenPointToRay(new Vector2(mousePosition.x, UnityEngine.Camera.current.pixelHeight - mousePosition.y));
            //float hit;
            //var position = xzPlane.Raycast(ray, out hit) ? ray.GetPoint(hit) : new Vector3();
            var position = UnityEngine.Camera.current.WorldToScreenPoint(layoutLink.Position.ToV3());

            Handles.BeginGUI();

            GUILayout.BeginArea(new Rect(position.x - 100, Screen.height - position.y - 100, 220, 200));
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(200));
            EditorGUILayout.TextField("IsLoaded", "True");
            GUILayout.EndVertical();
            GUILayout.EndArea();

            Handles.EndGUI();
        }

        public override void OnInspectorGUI()
        {
            var layoutLink = (LayoutLink) target;

            //var properties = layoutLink.PuzzleLayout.GetObjectAt(layoutLink.Position).Properties;

            //foreach (var property in properties)
            //{
            //    EditorGUILayout.TextField(property.Key, property.Value);
            //}
        }
    }
}
