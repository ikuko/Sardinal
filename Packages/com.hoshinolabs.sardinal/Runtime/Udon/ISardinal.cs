using System.Collections;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ISardinal : UdonSharpBehaviour {
        public virtual void Publish(SignalId id, object channel, params object[] args) { }
        public virtual void Subscribe(SignalId id, object channel, UdonSharpBehaviour subscriber) { }
    }
}
