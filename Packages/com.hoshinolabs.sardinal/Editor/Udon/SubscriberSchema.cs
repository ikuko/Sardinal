using System;

namespace HoshinoLabs.Sardinal.Udon {
    internal sealed class SubscriberSchema {
        public readonly string Signature;
        public readonly object Channel;
        public readonly Type Type;
        public readonly string MethodSymbol;
        public readonly string[] ParameterSymbols;
        public readonly bool Networked;

        public SubscriberSchema(string signature, object channel, Type type, string methodSymbol, string[] parameterSymbols, bool networked) {
            Signature = signature;
            Channel = channel;
            Type = type;
            MethodSymbol = methodSymbol;
            ParameterSymbols = parameterSymbols;
            Networked = networked;
        }
    }
}
