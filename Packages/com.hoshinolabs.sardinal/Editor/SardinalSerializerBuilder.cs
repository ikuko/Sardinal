using HoshinoLabs.Sardinal.Udon;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UdonSharp.Serialization;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalSerializerBuilder : IProcessSceneWithReport {
        public int callbackOrder => -5001;

        public void OnProcessScene(Scene scene, BuildReport report) {
            var typeCheckSerializersField = typeof(Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(SardinalSerializer));
            typeCheckSerializers.Insert(0, new SardinalSerializer(null));

            var typeSerializerDictionaryField = typeof(Serializer).GetField("_typeSerializerDictionary", BindingFlags.Static | BindingFlags.NonPublic);
            var typeSerializerDictionary = (Dictionary<TypeSerializationMetadata, Serializer>)typeSerializerDictionaryField.GetValue(null);
            typeSerializerDictionary.Remove(new TypeSerializationMetadata(ISardinal.ImplementationType));
        }
    }
}
