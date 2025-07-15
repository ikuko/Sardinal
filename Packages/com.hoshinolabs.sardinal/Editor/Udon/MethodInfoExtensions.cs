using HoshinoLabs.Sardinject.Udon;
using System;
using System.Linq;
using System.Reflection;

namespace HoshinoLabs.Sardinal.Udon {
    internal static class MethodInfoExtensions {
        public static SubscriberSchema ToSubscriberSchema(this MethodInfo self, Type declaringType) {
            var attribute = self.GetCustomAttribute<SubscriberAttribute>();
            var signature = $"{attribute.Topic.FullName.ComputeHashMD5()}.";
            foreach (var parameter in self.GetParameters()) {
                signature += $"__{parameter.ParameterType.FullName.Replace(".", "")}";
            }
            var channel = attribute.Channel;
#if VRC_SDK_WORLDS3_8_1_OR_NEWER
            var networked = self.IsDefined(typeof(VRC.SDK3.UdonNetworkCalling.NetworkCallableAttribute), false);
#else
            var networked = false;
#endif
            var methodSymbol = self.GetUdonExportSymbol(declaringType);
            var parameterSymbols = self.GetParameters()
                .Select(x => x.GetUdonExportSymbol(declaringType))
                .ToArray();
            var parameterTypes = self.GetParameters()
                .Select(x => $"__{x.ParameterType.FullName.Replace(".", "")}")
                .ToArray();
            return new SubscriberSchema(signature, channel, declaringType, methodSymbol, parameterSymbols, parameterTypes, networked);
        }
    }
}
