using HoshinoLabs.Sardinject;
using System;
using UdonSharp;
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
            var __1 = $"{((object[])(object)id)[0]}.";

            var length = args.Length;
            for (var i = 0; i < length; i++) {
                __1 += $"__{args[i].GetType().FullName.Replace(".", "")}";
            }

            var idx = Array.IndexOf(_0_1, __1);
            if (idx < 0) {
                return;
            }
            var __2 = _0_2[idx];
            var __3 = _0_3[idx];
            var __4 = _0_4[idx];
            var __5 = _0_5[idx];
            for (var i = 0; i < __2; i++) {
                var ___3 = __3[i];
                if (___3 != null && !___3.Equals(channel)) {
                    continue;
                }
                var ___4 = __4[i];
                var ___5 = __5[i];
                var ___5a = _1_3[___5];
                for (var j = 0; j < length; j++) {
                    ___4.SetProgramVariable(___5a[j], args[j]);
                }
                ___4.SendCustomEvent(_1_2[___5]);
            }
        }

        [Inject(Optional = true), SerializeField, HideInInspector]
        int _1_0;
        [Inject(Optional = true), SerializeField, HideInInspector]
        long[] _1_1;
        [Inject(Optional = true), SerializeField, HideInInspector]
        string[] _1_2;
        [Inject(Optional = true), SerializeField, HideInInspector]
        string[][] _1_3;

        public override void Subscribe(SignalId id, object channel, UdonSharpBehaviour subscriber) {
            if (id == null) {
                Debug.LogError("[<color=#47F1FF>Sardinal</color>] Attempting to use an invalid signal id");
                return;
            }
            var __1 = $"{((object[])(object)id)[0]}.";

            var ___5 = Array.IndexOf(_1_1, subscriber.GetUdonTypeID());
            for (var idx = 0; idx < _0_0; idx++) {
                if (!_0_1[idx].StartsWith(__1)) {
                    continue;
                }
                var __2 = _0_2[idx];
                var __2a = __2 + 1;
                _0_2[idx] = __2a;
                var __3 = _0_3[idx];
                var __3a = new object[__2a];
                Array.Copy(__3, __3a, __2);
                __3a[__2] = channel;
                _0_3[idx] = __3a;
                var __4 = _0_4[idx];
                var __4a = new IUdonEventReceiver[__2a];
                Array.Copy(__4, __4a, __2);
                __4a[__2] = (IUdonEventReceiver)(object)subscriber;
                _0_4[idx] = __4a;
                var __5 = _0_5[idx];
                var __5a = new int[__2a];
                Array.Copy(__5, __5a, __2);
                __5a[__2] = ___5;
                _0_5[idx] = __5a;
            }
        }
    }
}
