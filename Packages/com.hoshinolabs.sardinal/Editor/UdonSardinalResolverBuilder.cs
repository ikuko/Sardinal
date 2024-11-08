using HoshinoLabs.Sardinject;
using System.Collections.Generic;

namespace HoshinoLabs.Sardinal {
    internal sealed class UdonSardinalResolverBuilder : IResolverBuilder {
        public Dictionary<object, IResolver> Parameters { get; } = null;

        readonly ComponentDestination destination;

        public UdonSardinalResolverBuilder(ComponentDestination destination) {
            this.destination = destination;
        }

        public IResolver Build() {
            return new UdonSardinalResolver(destination);
        }
    }
}
