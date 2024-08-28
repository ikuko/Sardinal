using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoshinoLabs.Sardinal.Udon {
    [Serializable]
    public class SignalId : ISerializationCallbackReceiver {
        [SerializeField]
        public Type Topic;


        public SignalId(Type topic) {
            Topic = topic;
        }

        [SerializeField]
        string _serialized;

        public void OnAfterDeserialize() {

        }

        public void OnBeforeSerialize() {
            _serialized = Topic?.FullName;
        }
    }

    [Serializable]
    public class SignalId<T> : SignalId {
        public SignalId()
            : base(typeof(T)) {

        }
    }
}
