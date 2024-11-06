using System.Collections.Generic;
using System.Reflection;
using UdonSharp.Serialization;
using UnityEditor;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal static class SignalIdSerializerBuilder {
        [InitializeOnLoadMethod]
        static void OnLoad() {
            var typeCheckSerializersField = typeof(Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(SignalIdSerializer));
            typeCheckSerializers.Insert(0, new SignalIdSerializer(null));
        }
    }
}
