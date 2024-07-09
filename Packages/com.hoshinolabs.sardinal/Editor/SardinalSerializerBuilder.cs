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
            var _serializer = typeof(Serializer);
            var _typeCheckSerializers = _serializer.GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)_typeCheckSerializers.GetValue(_serializer);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(SardinalSerializer));
            typeCheckSerializers.Insert(0, new SardinalSerializer(null));
        }
    }
}
