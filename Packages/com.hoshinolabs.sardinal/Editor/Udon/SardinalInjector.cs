using HoshinoLabs.Sardinject;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HoshinoLabs.Sardinal.Udon {
    internal static class SardinalInjector {
        static GameObject rootGo;

        [InitializeOnLoadMethod]
        static void OnLoad() {
            var installersField = typeof(SardinalInstaller).GetField("Installers", BindingFlags.Static | BindingFlags.NonPublic);
            var installers = (Action<ContainerBuilder>)installersField.GetValue(null);
            installers -= OverrideSardinalInjection;
            installers += OverrideSardinalInjection;
            installersField.SetValue(null, installers);
        }

        static void OverrideSardinalInjection(ContainerBuilder builder) {
            builder.OverrideSardinalInjection()
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
