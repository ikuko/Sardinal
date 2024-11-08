using HoshinoLabs.Sardinject;
using UnityEditor;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal static class UdonSardinalInjector {
        static GameObject rootGo;

        [InitializeOnLoadMethod]
        static void OnLoad() {
            SardinalInstaller.Installers -= OverrideUdonSardinalInjection;
            SardinalInstaller.Installers += OverrideUdonSardinalInjection;
        }

        static void OverrideUdonSardinalInjection(ContainerBuilder builder) {
            builder.OverrideUdonSardinalInjection()
                .UnderTransform(() => {
                    if (rootGo == null) {
                        rootGo = new GameObject($"__{typeof(SardinalInstaller).Namespace.Replace('.', '_')}__");
                        rootGo.hideFlags = HideFlags.HideInHierarchy;
                    }
                    return rootGo.transform;
                });
        }
    }
}
