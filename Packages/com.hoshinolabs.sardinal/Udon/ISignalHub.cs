using System.Collections;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ISignalHub : UdonSharpBehaviour {
        public virtual void Publish(object signalId, params object[] args) {

        }

        //public virtual void Subscribe(object signalId, IUdonEventReceiver subscriber) {

        //}
    }
}
