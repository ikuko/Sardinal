using System;
using UnityEngine;

namespace HoshinoLabs.Sardinal.Udon {
    [Serializable]
    public class SignalId {
        [SerializeField]
        string topic;

        public string GetTopic() => topic;

        public SignalId(Type topic) {
            this.topic = topic.FullName;
        }
    }

    [Serializable]
    public class SignalId<T> : SignalId {
        public SignalId()
            : base(typeof(T)) {

        }
    }
}
