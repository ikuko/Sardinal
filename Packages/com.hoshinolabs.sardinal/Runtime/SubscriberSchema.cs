using System;
using System.Reflection;

namespace HoshinoLabs.Sardinal {
    internal sealed class SubscriberSchema {
        public readonly string Signature;
        public readonly object Channel;
        public readonly Type Type;
        public readonly MethodInfo MethodInfo;

        public SubscriberSchema(string signature, object channel, Type type, MethodInfo methodInfo) {
            Signature = signature;
            Channel = channel;
            Type = type;
            MethodInfo = methodInfo;
        }
    }
}
