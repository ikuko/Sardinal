namespace HoshinoLabs.Sardinal {
    internal sealed class SubscriberData {
        public readonly object Receiver;
        public readonly object Channel;
        public readonly SubscriberSchema Schema;

        public SubscriberData(object receiver, object channel, SubscriberSchema schema) {
            Receiver = receiver;
            Channel = channel;
            Schema = schema;
        }
    }
}
