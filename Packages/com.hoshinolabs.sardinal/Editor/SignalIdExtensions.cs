using HoshinoLabs.Sardinal.Udon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal static class SignalIdExtensions {
        public static object Pack(this SignalId self) {
            if (self.Topic == null) {
                return null;
            }
            return new[] {
                self.Topic.FullName.ComputeHashMD5(),
            };
        }

        public static SignalId UnPack(object obj) {
            return null;
        }
    }
}
