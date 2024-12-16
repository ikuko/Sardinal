using HoshinoLabs.Sardinject;
using System;
using UnityEditor;

namespace HoshinoLabs.Sardinal.Udon {
#if !COMPILER_UDONSHARP && UNITY_EDITOR
    internal class SardinalResolver {
        static Lazy<Sardinal> sardinal;

        [InitializeOnLoadMethod]
        static void OnLoad() {
            UnityInjector.OnProjectContainerBuilt -= ProjectContainerBuilt;
            UnityInjector.OnProjectContainerBuilt += ProjectContainerBuilt;
        }

        static void ProjectContainerBuilt(Container container) {
            sardinal = new(() => container.Resolve<Sardinal>());
        }

        public static Sardinal Resolve() {
            return sardinal.Value;
        }
    }
#endif
}
