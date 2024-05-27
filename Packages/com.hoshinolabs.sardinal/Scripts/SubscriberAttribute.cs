using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class SubscriberAttribute : Attribute {
        public Type BindTo { get; }

        public SubscriberAttribute(Type bindTo) {
            BindTo = bindTo;
        }
    }
}
