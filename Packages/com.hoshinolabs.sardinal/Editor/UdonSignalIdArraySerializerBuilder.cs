using System.Collections.Generic;
using System.Reflection;
using UdonSharp.Serialization;
using UnityEditor;

namespace HoshinoLabs.Sardinal {
    internal static class UdonSignalIdArraySerializerBuilder {
        [InitializeOnLoadMethod]
        static void OnLoad() {
            var typeCheckSerializersField = typeof(Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(UdonSignalIdArraySerializer));
            typeCheckSerializers.Insert(0, new UdonSignalIdArraySerializer(null));
        }
    }
}
