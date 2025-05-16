using System;

namespace HoshinoLabs.Sardinal {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class NetworkedAttribute : Attribute {
        public NetworkedAttribute() {

        }
    }
}
