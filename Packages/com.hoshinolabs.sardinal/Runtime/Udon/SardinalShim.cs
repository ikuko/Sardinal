using HoshinoLabs.Sardinject.Udon;
using System;
using UdonSharp;
using UnityEngine;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    internal sealed class SardinalShim : Sardinal {
        [Inject, SerializeField, HideInInspector]
        int _0;
        [Inject, SerializeField, HideInInspector]
        string[] _1;
        [Inject, SerializeField, HideInInspector]
        long[] _2;
        [Inject, SerializeField, HideInInspector]
        string[] _3;
        [Inject, SerializeField, HideInInspector]
        string[][] _4;
        [Inject, SerializeField, HideInInspector]
        string[][] _5;
        [Inject, SerializeField, HideInInspector]
        bool[] _6;
        [Inject, SerializeField, HideInInspector]
        int[] _7;
        [Inject, SerializeField, HideInInspector]
        object[][] _8;
        [Inject, SerializeField, HideInInspector]
        IUdonEventReceiver[][] _9;

        internal void Publish(string topic, object channel, params object[] args) {
            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                topic += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            var idx = Array.IndexOf(_1, topic);
            if (idx < 0) {
                return;
            }
            var __3 = _3[idx];
            var __4 = _4[idx];
            var __7 = _7[idx];
            var __8 = _8[idx];
            var __9 = _9[idx];
            for (var i = 0; i < __7; i++) {
                var ___8 = __8[i];
                if (channel != null && ___8 != null && !___8.Equals(channel)) {
                    continue;
                }
                var ___9 = __9[i];
                for (var j = 0; j < argsLength; j++) {
                    ___9.SetProgramVariable(__4[j], args[j]);
                }
                ___9.SendCustomEvent(__3);
            }
        }

        internal void Subscribe(string topic, object channel, UdonSharpBehaviour subscriber) {
            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _0; i++) {
                if (_2[i] != udonId) {
                    continue;
                }
                var __1 = _1[i];
                if (!__1.StartsWith(topic)) {
                    continue;
                }
                if (0 <= Array.IndexOf(_9[i], receiver)) {
                    continue;
                }
                var __7a = _7[i];
                var __7b = __7a + 1;
                _7[i] = __7b;
                var __8a = new object[__7b];
                Array.Copy(_8[i], __8a, __7a);
                __8a[__7a] = channel;
                _8[i] = __8a;
                var __9a = new IUdonEventReceiver[__7b];
                Array.Copy(_9[i], __9a, __7a);
                __9a[__7a] = receiver;
                _9[i] = __9a;
            }
        }

        internal void Unsubscribe(string topic, UdonSharpBehaviour subscriber) {
            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _0; i++) {
                if (_2[i] != udonId) {
                    continue;
                }
                var __1 = _1[i];
                if (!__1.StartsWith(topic)) {
                    continue;
                }
                var __9a = _9[i];
                var dest = Array.IndexOf(__9a, receiver);
                if (dest < 0) {
                    continue;
                }

                var __7a = _7[i];
                var __7b = __7a - 1;
                _7[i] = __7b;
                var src = dest + 1;
                var length = __7a - src;
                var __8a = _8[i];
                var __8b = new object[__7b];
                Array.Copy(__8a, 0, __8b, 0, dest);
                Array.Copy(__8a, src, __8b, dest, length);
                _8[i] = __8b;
                var __9b = new IUdonEventReceiver[__7b];
                Array.Copy(__9a, 0, __9b, 0, dest);
                Array.Copy(__9a, src, __9b, dest, length);
                _9[i] = __9b;
            }
        }
    }
}
