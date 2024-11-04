using System.Reflection;
using UdonSharp;
using VRC.Udon;

namespace HoshinoLabs.Sardinal {
    internal static class UdonSharpBehaviourExtensions {
        public static UdonBehaviour GetBackingUdonBehaviour(this UdonSharpBehaviour self) {
            var field = typeof(UdonSharpBehaviour).GetField("_udonSharpBackingUdonBehaviour", BindingFlags.NonPublic | BindingFlags.Instance);
            return (UdonBehaviour)field.GetValue(self);
        }
    }
}
