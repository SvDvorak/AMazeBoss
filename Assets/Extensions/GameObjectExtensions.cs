using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public static class GameObjectExtensions
    {
        public static GameObject Find(string name, bool isPartialName = false)
        {
            return GameObject
                .FindObjectsOfType<GameObject>()
                .First(x => MatchName(x.name, name, isPartialName));
        }

        public static IEnumerable<T> GetComponentsInChildren<T>(this GameObject gameObject, string name)
            where T : MonoBehaviour
        {
            var allComponents = gameObject.GetComponentsInChildren<T>();
            return allComponents.Where(x => x.name == name);
        }

        public static GameObject GetChild(this GameObject gameObject, string name, bool isPartialName = false)
        {
            return gameObject.GetChildren(name, isPartialName).FirstOrDefault();
        }

        public static IEnumerable<GameObject> GetChildren(this GameObject gameObject)
        {
            return gameObject.transform.Cast<Transform>().Select(x => x.gameObject);
        }

        public static IEnumerable<GameObject> GetChildren(this GameObject gameObject, string expectedName, bool isPartialName = false)
        {
            return gameObject
                .GetChildren()
                .Where(x => MatchName(x.name, expectedName, isPartialName));
        }

        public static bool NameContains(this GameObject gameObject, string toContain)
        {
            return MatchName(gameObject.name.ToUpper(), toContain.ToUpper(), true);
        }

        private static bool MatchName(string actualName, string expectedName, bool isPartialName)
        {
            return isPartialName ? actualName.Contains(expectedName) : actualName == expectedName;
        }
    }
}
