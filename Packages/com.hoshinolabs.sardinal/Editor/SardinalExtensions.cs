using System;

namespace HoshinoLabs.Sardinal {
    public static class SardinalExtensions {
        public static bool TryGetRuntimeSignalId(Type type, out object signalId) {
            signalId = type.FullName.ComputeHashMD5();
            return true;
        }

        public static bool TryGetRuntimeSignalId<T>(out object signalId) {
            return TryGetRuntimeSignalId(typeof(T), out signalId);
        }

        public static object GetRuntimeSignalId(Type type) {
            if (TryGetRuntimeSignalId(type, out var signalId)) {
                return signalId;
            }
            throw new Exception("Signal ID could not be found.");
        }

        public static object GetRuntimeSignalId<T>() {
            return GetRuntimeSignalId(typeof(T));
        }
    }
}
