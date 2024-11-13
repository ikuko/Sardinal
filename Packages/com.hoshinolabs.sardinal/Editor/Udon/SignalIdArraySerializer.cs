using System.Collections.Generic;
using System.Reflection;
using UdonSharp.Serialization;
using UnityEditor;

namespace HoshinoLabs.Sardinal.Udon {
    internal sealed class SignalIdArraySerializer : ArraySerializer<SignalId> {
        [InitializeOnLoadMethod]
        static void OnLoad() {
            var typeCheckSerializersField = typeof(Serializer).GetField("_typeCheckSerializers", BindingFlags.Static | BindingFlags.NonPublic);
            var typeCheckSerializers = (List<Serializer>)typeCheckSerializersField.GetValue(null);
            typeCheckSerializers.RemoveAll(x => x.GetType() == typeof(SignalIdArraySerializer));
            typeCheckSerializers.Insert(0, new SignalIdArraySerializer(null));
        }

        public SignalIdArraySerializer(TypeSerializationMetadata typeMetadata)
            : base(typeMetadata) {

        }

        protected override Serializer MakeSerializer(TypeSerializationMetadata typeMetadata) {
            VerifyTypeCheckSanity();
            return new SignalIdArraySerializer(typeMetadata);
        }

        public override object Serialize(in SignalId sourceObject) {
            return sourceObject.Pack();
        }

        public override SignalId Deserialize(object sourceObject) {
            return SignalIdExtensions.UnPack(sourceObject);
        }
    }
}
