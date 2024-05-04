using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal sealed class MethodSymbolData {
        public string MethodSymbol { get; }
        public string[] ParameterSymbols { get; }

        public MethodSymbolData(string methodSymbol, string[] parameterSymbols) {
            MethodSymbol = methodSymbol;
            ParameterSymbols = parameterSymbols;
        }
    }
}
