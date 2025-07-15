using System;
using System.Reflection;

namespace HoshinoLabs.Sardinal {
    internal static class MethodInfoExtensions {
        public static SubscriberSchema ToSubscriberSchema(this MethodInfo self, Type declaringType) {
            var attribute = self.GetCustomAttribute<SubscriberAttribute>();
            var signature = $"{attribute.Topic.FullName.ComputeHashMD5()}.";
            foreach (var parameter in self.GetParameters()) {
                signature += $"__{parameter.ParameterType.FullName.Replace(".", "")}";
            }
            var channel = attribute.Channel;
            return new SubscriberSchema(signature, channel, declaringType, self);
        }
    }
}
