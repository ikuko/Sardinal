using System;

namespace HoshinoLabs.Sardinal.Udon {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class SubscriberAttribute : Attribute {
        public readonly Type Topic;
        public readonly object Channel;

        public SubscriberAttribute(Type topic, object channel = null) {
            Topic = topic;
            Channel = channel;
        }
    }
}
