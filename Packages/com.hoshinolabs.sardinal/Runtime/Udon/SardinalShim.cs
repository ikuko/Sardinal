using System;
using UdonSharp;
using UnityEngine;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    internal sealed class SardinalShim : Sardinal {
        [SerializeField, HideInInspector]
        int _0;
        [SerializeField, HideInInspector]
        string[] _1;
        [SerializeField, HideInInspector]
        int[] _2;
        [SerializeField, HideInInspector]
        object[][] _3;
        [SerializeField, HideInInspector]
        IUdonEventReceiver[][] _4;
        [SerializeField, HideInInspector]
        int[][] _5;
        [SerializeField, HideInInspector]
        int _6;
        [SerializeField, HideInInspector]
        string[] _7;
        [SerializeField, HideInInspector]
        long[] _8;
        [SerializeField, HideInInspector]
        string[] _9;
        [SerializeField, HideInInspector]
        string[][] _10;

        public override void Publish(SignalId id, params object[] args) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                _id += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            var idx = Array.IndexOf(_1, _id);
            if (idx < 0) {
                return;
            }
            var __2 = _2[idx];
            var __3 = _3[idx];
            var __4 = _4[idx];
            var __5 = _5[idx];
            for (var i = 0; i < __2; i++) {
                var ___4 = __4[i];
                var ___5 = __5[i];
                var __10 = _10[___5];
                for (var j = 0; j < argsLength; j++) {
                    ___4.SetProgramVariable(__10[j], args[j]);
                }
                ___4.SendCustomEvent(_9[___5]);
            }
        }

        public override void PublishWithChannel(SignalId id, object channel, params object[] args) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var argsLength = args.Length;
            for (var i = 0; i < argsLength; i++) {
                _id += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            var idx = Array.IndexOf(_1, _id);
            if (idx < 0) {
                return;
            }
            var __2 = _2[idx];
            var __3 = _3[idx];
            var __4 = _4[idx];
            var __5 = _5[idx];
            for (var i = 0; i < __2; i++) {
                if (__3[i] != null && !__3[i].Equals(channel)) {
                    continue;
                }
                var ___4 = __4[i];
                var ___5 = __5[i];
                var __10 = _10[___5];
                for (var j = 0; j < argsLength; j++) {
                    ___4.SetProgramVariable(__10[j], args[j]);
                }
                ___4.SendCustomEvent(_9[___5]);
            }
        }

        public override void Subscribe(SignalId id, UdonSharpBehaviour subscriber) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _6; i++) {
                var __7 = _7[i];
                if (!__7.StartsWith(_id) || _8[i] != udonId) {
                    continue;
                }
                var idx = Array.IndexOf(_1, __7);
                if (idx < 0) {
                    var __0a = _0;
                    _0 = __0a + 1;
                    var __1a = new string[_0];
                    Array.Copy(_1, __1a, __0a);
                    __1a[__0a] = __7;
                    _1 = __1a;
                    var __2a = new int[_0];
                    Array.Copy(_2, __2a, __0a);
                    __2a[__0a] = 1;
                    _2 = __2a;
                    var __3a = new object[_0][];
                    Array.Copy(_3, __3a, __0a);
                    __3a[__0a] = new[] { (object)null };
                    _3 = __3a;
                    var __4a = new IUdonEventReceiver[_0][];
                    Array.Copy(_4, __4a, __0a);
                    __4a[__0a] = new[] { receiver };
                    _4 = __4a;
                    var __5a = new int[_0][];
                    Array.Copy(_5, __5a, __0a);
                    __5a[__0a] = new[] { i };
                    _5 = __5a;
                }
                else {
                    if (0 <= Array.IndexOf(_4[idx], receiver)) {
                        continue;
                    }
                    var __2a = _2[idx];
                    var __2b = __2a + 1;
                    _2[idx] = __2b;
                    var __3a = new object[__2b];
                    Array.Copy(_3[idx], __3a, __2a);
                    __3a[__2a] = null;
                    _3[idx] = __3a;
                    var __4a = new IUdonEventReceiver[__2b];
                    Array.Copy(_4[idx], __4a, __2a);
                    __4a[__2a] = receiver;
                    _4[idx] = __4a;
                    var __5a = new int[__2b];
                    Array.Copy(_5[idx], __5a, __2a);
                    __5a[__2a] = i;
                    _5[idx] = __5a;
                }
            }
        }

        public override void SubscribeWithChannel(SignalId id, object channel, UdonSharpBehaviour subscriber) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _6; i++) {
                var __1 = _7[i];
                if (!__1.StartsWith(_id) || _8[i] != udonId) {
                    continue;
                }
                var idx = Array.IndexOf(_1, __1);
                if (idx < 0) {
                    var __0a = _0;
                    _0 = __0a + 1;
                    var __1a = new string[_0];
                    Array.Copy(_1, __1a, __0a);
                    __1a[__0a] = __1;
                    _1 = __1a;
                    var __2a = new int[_0];
                    Array.Copy(_2, __2a, __0a);
                    __2a[__0a] = 1;
                    _2 = __2a;
                    var __3a = new object[_0][];
                    Array.Copy(_3, __3a, __0a);
                    __3a[__0a] = new[] { channel };
                    _3 = __3a;
                    var __4a = new IUdonEventReceiver[_0][];
                    Array.Copy(_4, __4a, __0a);
                    __4a[__0a] = new[] { receiver };
                    _4 = __4a;
                    var __5a = new int[_0][];
                    Array.Copy(_5, __5a, __0a);
                    __5a[__0a] = new[] { i };
                    _5 = __5a;
                }
                else {
                    if (0 <= Array.IndexOf(_4[idx], receiver)) {
                        continue;
                    }
                    var __2a = _2[idx];
                    var __2b = __2a + 1;
                    _2[idx] = __2b;
                    var __3a = new object[__2b];
                    Array.Copy(_3[idx], __3a, __2a);
                    __3a[__2a] = channel;
                    _3[idx] = __3a;
                    var __4a = new IUdonEventReceiver[__2b];
                    Array.Copy(_4[idx], __4a, __2a);
                    __4a[__2a] = receiver;
                    _4[idx] = __4a;
                    var __5a = new int[__2b];
                    Array.Copy(_5[idx], __5a, __2a);
                    __5a[__2a] = i;
                    _5[idx] = __5a;
                }
            }
        }

        public override void Unsubscribe(SignalId id, UdonSharpBehaviour subscriber) {
            if (id == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _id = $"{((object[])(object)id)[0]}.";

            var udonId = subscriber.GetUdonTypeID();
            var receiver = (IUdonEventReceiver)(object)subscriber;
            for (var i = 0; i < _6; i++) {
                var __1 = _7[i];
                if (!__1.StartsWith(_id) || _8[i] != udonId) {
                    continue;
                }
                var idx = Array.IndexOf(_1, __1);
                if (idx < 0) {
                    continue;
                }
                var __4a = _4[idx];
                var dest = Array.IndexOf(__4a, receiver);
                if(dest < 0) {
                    continue;
                }
                var __2a = _2[idx];
                var __2b = __2a - 1;
                _2[idx] = __2b;
                var src = dest + 1;
                var length = __2a - src;
                var __3a = _3[idx];
                var __3b = new object[__2b];
                Array.Copy(__3a, 0, __3b, 0, dest);
                Array.Copy(__3a, src, __3b, dest, length);
                _3[idx] = __3b;
                var __4b = new IUdonEventReceiver[__2b];
                Array.Copy(__4a, 0, __4b, 0, dest);
                Array.Copy(__4a, src, __4b, dest, length);
                _4[idx] = __4b;
                var __5a = _5[idx];
                var __5b = new int[__2b];
                Array.Copy(__5a, 0, __5b, 0, dest);
                Array.Copy(__5a, src, __5b, dest, length);
                _5[idx] = __5b;
            }
        }
    }
}
