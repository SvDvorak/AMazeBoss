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

        public static void CallEvent<T1, T2>(this Action<T1, T2> callback, T1 value1, T2 value2)
        {
            if (callback != null)
            {
                callback(value1, value2);
            }
        }

        public static void CallEvent<T1, T2, T3>(this Action<T1, T2, T3> callback, T1 value1, T2 value2, T3 value3)
        {
            if (callback != null)
            {
                callback(value1, value2, value3);
            }
        }
    }
}