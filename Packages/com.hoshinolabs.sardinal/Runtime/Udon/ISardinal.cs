using System;
using UdonSharp;
using UnityEngine;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ISardinal : UdonSharpBehaviour {
        public static Type ImplementationType => typeof(Sardinal);

        public virtual void Publish(SignalId id, params object[] args) { }
        public virtual void PublishWithChannel(SignalId id, object channel, params object[] args) { }
        public virtual void Subscribe(SignalId id, UdonSharpBehaviour subscriber) { }
        public virtual void SubscribeWithChannel(SignalId id, object channel, UdonSharpBehaviour subscriber) { }
        public virtual void Unsubscribe(SignalId id, UdonSharpBehaviour subscriber) { }
    }
}
