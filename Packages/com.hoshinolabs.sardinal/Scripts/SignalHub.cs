using HoshinoLabs.Sardinal.Udon;
using System.Collections;
using System.Collections.Generic;
using UdonSharp;
using UnityEngine;
using VRC.Udon.Common.Interfaces;

namespace Sardinal.Udon {
    public static class SignalHub {
        //[Inject, SerializeField, HideInInspector]
        //string[] _0;
        //static string[] _0;

        //public static void Publish<T>(params string[] args) where T : ISignal {
        //    var className = UdonSharpBehaviour.GetUdonTypeName<T>();
        //    var _length = args.Length - 1;
        //    var _args = new string[_length];
        //    for (var i = 0; i < _length; i++) {
        //        _args[i] = $"\"{args[i + 1]}\"";
        //    }
        //    Debug.Log($"Publish<{className}>({string.Join(", ", _args)})");


        //    //var idx = Array.IndexOf(_0, type);
        //    //if (idx < 0) {
        //    //    Debug.LogError($"[<color=#47F1FF>Sardinject</color>] Unable to resolve for type `{type}`.");
        //    //    return null;
        //    //}
        //}

        //public static void Subscribe<T>(IUdonEventReceiver subscriber) where T : ISignal {
        //    //var className = UdonSharpBehaviour.GetUdonTypeName<CustomSignal>();
        //    //Debug.Log($"MySubscriber<{className}>({a}, {b})");
        //}
    }
}
