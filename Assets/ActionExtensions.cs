using System;

namespace Assets
{
    public static class ActionExtensions
    {
        public static void CallEvent(this Action callback)
        {
            if (callback != null)
            {
                callback();
            }
        }

        public static void CallEvent<T>(this Action<T> callback, T value)
        {
            if (callback != null)
            {
                callback(value);
            }
        }
    }
}