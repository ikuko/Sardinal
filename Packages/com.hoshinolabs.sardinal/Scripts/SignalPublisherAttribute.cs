using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SignalPublisherAttribute : Attribute {
        public Type BindTo { get; }

        public SignalPublisherAttribute(Type bindTo) {
            BindTo = bindTo;
        }
    }
}
