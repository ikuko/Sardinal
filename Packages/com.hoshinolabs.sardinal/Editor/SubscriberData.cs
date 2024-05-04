using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.Udon;

namespace HoshinoLabs.Sardinal {
    internal sealed class SubscriberData {
        public UdonBehaviour Udon { get; }
        public MethodSymbolData MethodSymbolData { get; }

        public SubscriberData(UdonBehaviour udon, MethodSymbolData methodSymbolData) {
            Udon = udon;
            MethodSymbolData = methodSymbolData;
        }
    }
}
