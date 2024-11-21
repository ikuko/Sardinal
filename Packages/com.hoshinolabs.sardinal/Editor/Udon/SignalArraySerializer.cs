using HoshinoLabs.Sardinject.Udon;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace HoshinoLabs.Sardinal.Udon {
    internal sealed class SignalArraySerializer : UdonSharp.Serialization.Serializer {
        [InitializeOnLoadMethod]
        static void OnLoad() {
            var typeCheckSerializersField = typeof(UdonSharp.Serialization.Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<UdonSharp.Serialization.Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(SignalSerializer));
            typeCheckSerializers.Insert(0, new SignalSerializer(null));
        }

        public SignalArraySerializer(UdonSharp.Serialization.TypeSerializationMetadata typeMetadata)
            : base(typeMetadata) {

        }

        protected override UdonSharp.Serialization.Serializer MakeSerializer(UdonSharp.Serialization.TypeSerializationMetadata typeMetadata) {
            var type = typeof(Serializer<>).MakeGenericType(typeMetadata.cSharpType);
            return (UdonSharp.Serialization.Serializer)Activator.CreateInstance(type, new object[] { typeMetadata });
        }

        protected override bool HandlesTypeSerialization(UdonSharp.Serialization.TypeSerializationMetadata typeMetadata) {
            VerifyTypeCheckSanity();
            if (!typeMetadata.cSharpType.IsConstructedGenericType) {
                return false;
            }
            return typeMetadata.cSharpType.GetGenericTypeDefinition() == typeof(Signal<>);
        }

        public override void WriteWeak(UdonSharp.Serialization.IValueStorage targetObject, object sourceObject) {
            throw new NotImplementedException();
        }

        public override void ReadWeak(ref object targetObject, UdonSharp.Serialization.IValueStorage sourceObject) {
            throw new NotImplementedException();
        }

        public override Type GetUdonStorageType() {
            throw new NotImplementedException();
        }
    }
}
