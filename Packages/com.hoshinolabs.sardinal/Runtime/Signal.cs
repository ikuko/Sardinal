using System;

namespace HoshinoLabs.Sardinal {
    [Serializable]
    public class Signal {
        Sardinal sardinal;
        Type topic;
        object channel;

        public Signal() {
            sardinal = SardinalResolver.Resolve();
        }

        public Signal(Type topic) {
            sardinal = SardinalResolver.Resolve();
            this.topic = topic;
        }

        public Signal WithChannel(object channel) {
            var signal = new Signal() {
                topic = topic,
                channel = channel
            };
            return signal;
        }

        public void Publish(params object[] args) {
            sardinal.Publish($"{topic.FullName.ComputeHashMD5()}.", channel, args);
        }

        public void Subscribe(object subscriber) {
            sardinal.Subscribe(channel, subscriber);
        }

        public void Unsubscribe(object subscriber) {
            sardinal.Unsubscribe(subscriber);
        }
    }

    [Serializable]
    public sealed class Signal<T> : Signal {
        public Signal()
            : base(typeof(T)) {

        }
    }
}
