using HoshinoLabs.Sardinject;
using System;
using UdonSharp;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    internal sealed class Sardinal : ISardinal {
        [Inject(Optional = true), SerializeField, HideInInspector]
        int _0_0;
        [Inject(Optional = true), SerializeField, HideInInspector]
        string[] _0_1;
        [Inject(Optional = true), SerializeField, HideInInspector]
        int[] _0_2;
        [Inject(Optional = true), SerializeField, HideInInspector]
        object[][] _0_3;
        [Inject(Optional = true), SerializeField, HideInInspector]
        IUdonEventReceiver[][] _0_4;
        [Inject(Optional = true), SerializeField, HideInInspector]
        int[][] _0_5;

        public override void Publish(SignalId id, object channel, params object[] args) {
            if (id == null) {
                Debug.LogError("[<color=#47F1FF>Sardinal</color>] Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                _id += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            var idx = Array.IndexOf(_0_1, _id);
            if (idx < 0) {
                return;
            }
            var __2 = _0_2[idx];
            var __3 = _0_3[idx];
            var __4 = _0_4[idx];
            var __5 = _0_5[idx];
            for (var i = 0; i < __2; i++) {
                if (__3[i] != null && !__3[i].Equals(channel)) {
                    continue;
                }
                var ___4 = __4[i];
                var ___5 = __5[i];
                var __1_4 = _1_4[___5];
                for (var j = 0; j < argsLength; j++) {
                    ___4.SetProgramVariable(__1_4[j], args[j]);
                }
                ___4.SendCustomEvent(_1_3[___5]);
            }
        }

        [Inject(Optional = true), SerializeField, HideInInspector]
        int _1_0;
        [Inject(Optional = true), SerializeField, HideInInspector]
        string[] _1_1;
        [Inject(Optional = true), SerializeField, HideInInspector]
        long[] _1_2;
        [Inject(Optional = true), SerializeField, HideInInspector]
        string[] _1_3;
        [Inject(Optional = true), SerializeField, HideInInspector]
        string[][] _1_4;

        public override void Subscribe(SignalId id, object channel, UdonSharpBehaviour subscriber) {
            if (id == null) {
                Debug.LogError("[<color=#47F1FF>Sardinal</color>] Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _1_0; i++) {
                var __1 = _1_1[i];
                if (!__1.StartsWith(_id) || _1_2[i] != udonId) {
                    continue;
                }
                var idx = Array.IndexOf(_0_1, __1);
                if (idx < 0) {
                    var _0_0a = _0_0;
                    _0_0 = _0_0a + 1;
                    var _0_1a = new string[_0_0];
                    Array.Copy(_0_1, _0_1a, _0_0a);
                    _0_1a[_0_0a] = __1;
                    _0_1 = _0_1a;
                    var _0_2a = new int[_0_0];
                    Array.Copy(_0_2, _0_2a, _0_0a);
                    _0_2a[_0_0a] = 1;
                    _0_2 = _0_2a;
                    var _0_3a = new object[_0_0][];
                    Array.Copy(_0_3, _0_3a, _0_0a);
                    _0_3a[_0_0a] = new[] { channel };
                    _0_3 = _0_3a;
                    var _0_4a = new IUdonEventReceiver[_0_0][];
                    Array.Copy(_0_4, _0_4a, _0_0a);
                    _0_4a[_0_0a] = new[] { receiver };
                    _0_4 = _0_4a;
                    var _0_5a = new int[_0_0][];
                    Array.Copy(_0_5, _0_5a, _0_0a);
                    _0_5a[_0_0a] = new[] { i };
                    _0_5 = _0_5a;
                }
                else {
                    if (0 <= Array.IndexOf(_0_4[idx], receiver)) {
                        continue;
                    }
                    var _0_2a = _0_2[idx];
                    var _0_2b = _0_2a + 1;
                    _0_2[idx] = _0_2b;
                    var _0_3a = new object[_0_2b];
                    Array.Copy(_0_3[idx], _0_3a, _0_2a);
                    _0_3a[_0_2a] = channel;
                    _0_3[idx] = _0_3a;
                    var _0_4a = new IUdonEventReceiver[_0_2b];
                    Array.Copy(_0_4[idx], _0_4a, _0_2a);
                    _0_4a[_0_2a] = receiver;
                    _0_4[idx] = _0_4a;
                    var _0_5a = new int[_0_2b];
                    Array.Copy(_0_5[idx], _0_5a, _0_2a);
                    _0_5a[_0_2a] = i;
                    _0_5[idx] = _0_5a;
                }
            }
        }
        public override void Unsubscribe(SignalId id, UdonSharpBehaviour subscriber) {
            if (id == null) {
                Debug.LogError("[<color=#47F1FF>Sardinal</color>] Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _1_0; i++) {
                var __1 = _1_1[i];
                if (!__1.StartsWith(_id) || _1_2[i] != udonId) {
                    continue;
                }
                var idx = Array.IndexOf(_0_1, __1);
                if (idx < 0) {
                    continue;
                }
                var _0_4a = _0_4[idx];
                var dest = Array.IndexOf(_0_4a, receiver);
                if(dest < 0) {
                    continue;
                }
                var _0_2a = _0_2[idx];
                var _0_2b = _0_2a - 1;
                _0_2[idx] = _0_2b;
                var src = dest + 1;
                var length = _0_2a - src;
                var _0_3a = _0_3[idx];
                var _0_3b = new object[_0_2b];
                Array.Copy(_0_3a, 0, _0_3b, 0, dest);
                Array.Copy(_0_3a, src, _0_3b, dest, length);
                _0_3[idx] = _0_3b;
                var _0_4b = new IUdonEventReceiver[_0_2b];
                Array.Copy(_0_4a, 0, _0_4b, 0, dest);
                Array.Copy(_0_4a, src, _0_4b, dest, length);
                _0_4[idx] = _0_4b;
                var _0_5a = _0_5[idx];
                var _0_5b = new int[_0_2b];
                Array.Copy(_0_5a, 0, _0_5b, 0, dest);
                Array.Copy(_0_5a, src, _0_5b, dest, length);
                _0_5[idx] = _0_5b;
            }
        }
    }
}
