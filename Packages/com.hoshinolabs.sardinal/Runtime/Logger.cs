using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal static class Logger {
        public static void Log(object message) {
            Debug.Log($"[<color=#47F1FF>Sardinal</color>] {message}");
        }

        public static void Log(object message, Object context) {
            Debug.Log($"[<color=#47F1FF>Sardinal</color>] {message}", context);
        }

        public static void LogError(object message) {
            Debug.LogError($"[<color=#47F1FF>Sardinal</color>] {message}");
        }

        public static void LogError(object message, Object context) {
            Debug.LogError($"[<color=#47F1FF>Sardinal</color>] {message}", context);
        }

        public static void LogWarning(object message) {
            Debug.LogWarning($"[<color=#47F1FF>Sardinal</color>] {message}");
        }

        public static void LogWarning(object message, Object context) {
            Debug.LogWarning($"[<color=#47F1FF>Sardinal</color>] {message}", context);
        }
    }
}
