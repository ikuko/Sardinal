using System;

namespace HoshinoLabs.Sardinal {
    internal sealed class SubscriberSchema {
        public string Signature { get; }
        public object Channel { get; }
        public Type Type { get; }
        public string MethodSymbol { get; }
        public string[] ParameterSymbols { get; }

        public SubscriberSchema(string signature, object channel, Type type, string methodSymbol, string[] parameterSymbols) {
            Signature = signature;
            Channel = channel;
            Type = type;
            MethodSymbol = methodSymbol;
            ParameterSymbols = parameterSymbols;
        }
    }
}
