using HoshinoLabs.Sardinject;
using System;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    public class SardinalInstaller : MonoBehaviour, IInstaller {
        internal static event Action<ContainerBuilder> Installers;

        public void Install(ContainerBuilder builder) {
            builder.OverrideSardinalInjection();
            builder.Register(typeof(Signal<>), Lifetime.Transient);
            Installers?.Invoke(builder);
        }
    }
}
