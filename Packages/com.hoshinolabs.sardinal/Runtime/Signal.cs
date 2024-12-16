using System;

namespace HoshinoLabs.Sardinal {
    [Serializable]
    public sealed class Signal<T> {
        Sardinal sardinal;

        public Signal() {
            sardinal = SardinalResolver.Resolve();
        }

        object channel;

        public Signal<T> WithChannel(object channel) {
            this.channel = channel;
            return this;
        }

        public void Publish(params object[] args) {
            sardinal.Publish($"{typeof(T).FullName.ComputeHashMD5()}.", channel, args);
        }

        public void Subscribe(object subscriber) {
            sardinal.Subscribe(channel, subscriber);
        }

        public void Unsubscribe(object subscriber) {
            sardinal.Unsubscribe(subscriber);
        }
    }
}
