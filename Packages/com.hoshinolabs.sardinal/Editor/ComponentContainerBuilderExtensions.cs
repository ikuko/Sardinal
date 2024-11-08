#if UDONSHARP
using HoshinoLabs.Sardinal.Udon;
using HoshinoLabs.Sardinject;

namespace HoshinoLabs.Sardinal {
    internal static class ComponentContainerBuilderExtensions {
        public static ComponentBindingBuilder OverrideUdonSardinalInjection(this ContainerBuilder self) {
            var destination = new ComponentDestination();
            var resolverBuilder = new UdonSardinalResolverBuilder(destination).OverrideScopeIfNeeded(self, Lifetime.Cached);
            var builder = new ComponentBindingBuilder(typeof(ISardinal), resolverBuilder, destination);
            self.Register(builder);
            return builder;
        }
    }
}
#endif
