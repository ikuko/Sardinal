using HoshinoLabs.Sardinject;

namespace HoshinoLabs.Sardinal.Udon {
    internal static class ComponentContainerBuilderExtensions {
        public static ComponentBindingBuilder OverrideSardinalInjection(this ContainerBuilder self) {
            var destination = new ComponentDestination();
            var resolverBuilder = new SardinalResolverBuilder(destination).OverrideScopeIfNeeded(self, Lifetime.Cached);
            var builder = new ComponentBindingBuilder(typeof(Sardinal), resolverBuilder, destination);
            self.Register(builder);
            return builder;
        }
    }
}
