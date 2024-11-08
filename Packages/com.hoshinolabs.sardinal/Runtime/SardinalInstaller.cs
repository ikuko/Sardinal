using HoshinoLabs.Sardinject;
using System;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    public class SardinalInstaller : MonoBehaviour, IInstaller {
        public static event Action<ContainerBuilder> Installers;

        public void Install(ContainerBuilder builder) {
            builder.OverrideSardinalInjection();
            Installers?.Invoke(builder);
        }
    }
}
