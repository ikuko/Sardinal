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

        internal void Publish(string topic, object channel, params object[] args) {
            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                topic += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            if (!subscriberData.TryGetValue(topic, out var data)) {
                return;
            }
            foreach (var x in data.Where(x => channel == null || x.Channel == null || x.Channel.Equals(channel))) {
                x.Schema.MethodInfo.Invoke(x.Receiver, args);
            }
        }

        internal void Subscribe(object channel, object subscriber) {
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

        internal void Unsubscribe(object subscriber) {
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
