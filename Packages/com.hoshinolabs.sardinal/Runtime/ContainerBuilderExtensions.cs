using HoshinoLabs.Sardinject;

namespace HoshinoLabs.Sardinal {
    internal static class ContainerBuilderExtensions {
        public static BindingBuilder OverrideSardinalInjection(this ContainerBuilder self) {
            var resolverBuilder = new SardinalResolverBuilder().OverrideScopeIfNeeded(self, Lifetime.Cached);
            var builder = new BindingBuilder(typeof(Sardinal), resolverBuilder);
            self.Register(builder);
            return builder;
        }
    }
}
