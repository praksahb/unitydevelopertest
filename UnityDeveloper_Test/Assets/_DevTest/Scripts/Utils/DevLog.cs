using UnityEngine;

namespace DevTest
{
    public static class DevLog
    {
        public static void Info(object message)
        {
            Debug.Log(message);
        }

        public static void Warn(object message)
        {
            Debug.LogWarning(message);
        }

        public static void Error(object message)
        {
            Debug.LogError(message);
        }
    }
}
