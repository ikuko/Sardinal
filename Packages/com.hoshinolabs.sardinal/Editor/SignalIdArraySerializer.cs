using UdonSharp.Serialization;

namespace HoshinoLabs.Sardinal {
    internal sealed class SignalIdArraySerializer : ArraySerializer<SignalId> {
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
