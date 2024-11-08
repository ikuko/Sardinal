using HoshinoLabs.Sardinject;
using System.Collections.Generic;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalResolverBuilder : IResolverBuilder {
        public Dictionary<object, IResolver> Parameters { get; } = null;

        public SardinalResolverBuilder() {

        }

        public IResolver Build() {
            return new SardinalResolver();
        }
    }
}
