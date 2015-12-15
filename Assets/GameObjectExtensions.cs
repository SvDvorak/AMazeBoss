using UnityEngine;

namespace Assets
{
    public static class GameObjectExtensions
    {
        public static bool NameContains(this GameObject gameObject, string toContain)
        {
            return gameObject.name.ToUpper().Contains(toContain.ToUpper());
        }
    }
}
