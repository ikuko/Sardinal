using UdonSharp.Serialization;

namespace HoshinoLabs.Sardinal {
    internal sealed class UdonSignalIdArraySerializer : ArraySerializer<Udon.SignalId> {
        public UdonSignalIdArraySerializer(TypeSerializationMetadata typeMetadata)
            : base(typeMetadata) {

        }

        protected override Serializer MakeSerializer(TypeSerializationMetadata typeMetadata) {
            VerifyTypeCheckSanity();
            return new UdonSignalIdArraySerializer(typeMetadata);
        }

        public override object Serialize(in Udon.SignalId sourceObject) {
            return sourceObject.Pack();
        }

        public override Udon.SignalId Deserialize(object sourceObject) {
            return UdonSignalIdExtensions.UnPack(sourceObject);
        }
    }
}
