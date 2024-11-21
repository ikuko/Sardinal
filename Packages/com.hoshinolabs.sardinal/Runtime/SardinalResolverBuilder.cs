using HoshinoLabs.Sardinject;
using System.Collections.Generic;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalResolverBuilder : IResolverBuilder {
        public Dictionary<object, IResolver> Parameters { get; } = new();

        public SardinalResolverBuilder() {

        }

        public IBindingResolver Build() {
            return new SardinalResolver();
        }
    }
}
