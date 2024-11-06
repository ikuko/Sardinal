using System.Collections.Generic;
using System.Reflection;
using UdonSharp.Serialization;
using UnityEditor;

namespace HoshinoLabs.Sardinal {
    internal static class SignalIdArraySerializerBuilder {
        [InitializeOnLoadMethod]
        static void OnLoad() {
            var typeCheckSerializersField = typeof(Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(SignalIdArraySerializer));
            typeCheckSerializers.Insert(0, new SignalIdArraySerializer(null));
        }
    }
}
