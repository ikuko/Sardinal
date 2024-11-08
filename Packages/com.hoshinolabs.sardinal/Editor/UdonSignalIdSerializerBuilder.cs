using System.Collections.Generic;
using System.Reflection;
using UdonSharp.Serialization;
using UnityEditor;

namespace HoshinoLabs.Sardinal {
    internal static class UdonSignalIdSerializerBuilder {
        [InitializeOnLoadMethod]
        static void OnLoad() {
            var typeCheckSerializersField = typeof(Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(UdonSignalIdSerializer));
            typeCheckSerializers.Insert(0, new UdonSignalIdSerializer(null));
        }
    }
}
