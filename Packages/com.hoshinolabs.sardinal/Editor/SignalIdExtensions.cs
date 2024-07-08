using HoshinoLabs.Sardinal.Udon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal static class SignalIdExtensions {
        public static object Pack(this SignalId self) {
            if (self.BindTo == null) {
                return null;
            }
            return new[] {
                self.BindTo.FullName.ComputeHashMD5(),
            };
        }

        public static SignalId UnPack(object obj) {
            return null;
        }
    }
}
