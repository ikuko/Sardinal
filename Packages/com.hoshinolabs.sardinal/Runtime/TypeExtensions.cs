using HoshinoLabs.Sardinject;
using System;
using System.Linq;
using System.Reflection;

namespace HoshinoLabs.Sardinal {
    internal static class TypeExtensions {
        public static SubscriberSchema[] GetSubscriberSchemas(this Type self) {
            return self.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.IsDefined(typeof(SubscriberAttribute), false))
                .Select(x => x.ToSubscriberSchema(self))
                .Concat(self.BaseType?.GetSubscriberSchemas() ?? Array.Empty<SubscriberSchema>())
                .ToArray();
        }
    }
}
