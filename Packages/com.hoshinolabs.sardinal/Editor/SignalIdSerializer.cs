using UdonSharp.Serialization;

namespace HoshinoLabs.Sardinal {
    internal sealed class SignalIdSerializer : Serializer<SignalId> {
        public SignalIdSerializer(TypeSerializationMetadata typeMetadata)
            : base(typeMetadata) {

        }

        protected override Serializer MakeSerializer(TypeSerializationMetadata typeMetadata) {
            VerifyTypeCheckSanity();
            return new SignalIdSerializer(typeMetadata);
        }

        public override object Serialize(in SignalId sourceObject) {
            return sourceObject.Pack();
        }

        public override SignalId Deserialize(object sourceObject) {
            return SignalIdExtensions.UnPack(sourceObject);
        }
    }
}
