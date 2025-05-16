using System;

namespace HoshinoLabs.Sardinal.Udon {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class NetworkedAttribute : Attribute {
        public NetworkedAttribute() {

        }
    }
}
