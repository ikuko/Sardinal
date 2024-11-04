using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal {
    internal sealed class SubscriberData {
        public string Signature { get; }
        public IUdonEventReceiver Receiver { get; }
        public object Channel { get; }
        public int SchemaId { get; }

        public SubscriberData(string signature, IUdonEventReceiver receiver, object channel, int schemaId) {
            Signature = signature;
            Receiver = receiver;
            Channel = channel;
            SchemaId = schemaId;
        }
    }
}
