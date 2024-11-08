using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal {
    internal sealed class SubscriberData {
        public readonly string Signature;
        public readonly IUdonEventReceiver Receiver;
        public readonly object Channel;
        public readonly int SchemaId;

        public SubscriberData(string signature, IUdonEventReceiver receiver, object channel, int schemaId) {
            Signature = signature;
            Receiver = receiver;
            Channel = channel;
            SchemaId = schemaId;
        }
    }
}
