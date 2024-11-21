using HoshinoLabs.Sardinject;
using System.Collections.Generic;

namespace HoshinoLabs.Sardinal.Udon {
    internal sealed class SardinalResolverBuilder : IResolverBuilder {
        public Dictionary<object, IResolver> Parameters { get; } = null;

        readonly ComponentDestination destination;

        public SardinalResolverBuilder(ComponentDestination destination) {
            this.destination = destination;
        }

        public IBindingResolver Build() {
            return new SardinalResolver(destination);
        }
    }
}
