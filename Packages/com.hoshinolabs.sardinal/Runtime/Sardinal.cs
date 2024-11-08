using System;
using System.Collections.Generic;
using System.Linq;

namespace HoshinoLabs.Sardinal {
    public class Sardinal {
        readonly IReadOnlyDictionary<Type, List<SubscriberSchema>> subscriberSchema;
        readonly Dictionary<string, List<SubscriberData>> subscriberData;

        internal Sardinal(IReadOnlyDictionary<Type, List<SubscriberSchema>> subscriberSchema, Dictionary<string, List<SubscriberData>> subscriberData) {
            this.subscriberSchema = subscriberSchema;
            this.subscriberData = subscriberData;
        }

        public void Publish(SignalId id, params object[] args) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{id.GetTopic().ComputeHashMD5()}.";

            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                _id += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            if (!subscriberData.TryGetValue(_id, out var data)) {
                return;
            }
            foreach (var x in data) {
                x.Schema.MethodInfo.Invoke(x.Receiver, args);
            }
        }

        public void PublishWithChannel(SignalId id, object channel, params object[] args) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{id.GetTopic().ComputeHashMD5()}.";

            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                _id += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            if (!subscriberData.TryGetValue(_id, out var data)) {
                return;
            }
            foreach (var x in data.Where(x => x.Channel == null || x.Channel.Equals(channel))) {
                x.Schema.MethodInfo.Invoke(x.Receiver, args);
            }
        }

        public void Subscribe(object subscriber) {
            if (!subscriberSchema.TryGetValue(subscriber.GetType(), out var schemas)) {
                return;
            }
            foreach (var x in schemas) {
                if (!subscriberData.TryGetValue(x.Signature, out var data)) {
                    data = new();
                    subscriberData.Add(x.Signature, data);
                }
                data.Add(new SubscriberData(subscriber, null, x));
            }
        }

        public void SubscribeWithChannel(object channel, object subscriber) {
            if (!subscriberSchema.TryGetValue(subscriber.GetType(), out var schemas)) {
                return;
            }
            foreach (var x in schemas) {
                if (!subscriberData.TryGetValue(x.Signature, out var data)) {
                    data = new();
                    subscriberData.Add(x.Signature, data);
                }
                data.Add(new SubscriberData(subscriber, channel, x));
            }
        }

        public void Unsubscribe(object subscriber) {
            if (!subscriberSchema.TryGetValue(subscriber.GetType(), out var schemas)) {
                return;
            }
            foreach (var x in schemas) {
                if (!subscriberData.TryGetValue(x.Signature, out var data)) {
                    data = new();
                    subscriberData.Add(x.Signature, data);
                }
                data.RemoveAll(x => x.Receiver == subscriber);
            }
        }
    }
}
