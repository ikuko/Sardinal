using HoshinoLabs.Sardinject.Udon;
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace HoshinoLabs.Sardinal.Udon {
    [AddComponentMenu("")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
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
            var __6 = _6[idx];
            if (__6) {
                var __5 = _5[idx];
                var _channel = channel == null ? null : channel.ToString();
                var _args = new string[argsLength];
                var _urls = new VRCUrl[argsLength];
                for (var i = 0; i < argsLength; i++) {
                    var ___5 = __5[i];
                    switch (___5) {
                        case "__SystemBoolean":
                        case "__SystemByte":
                        case "__SystemInt16":
                        case "__SystemUInt16":
                        case "__SystemInt32":
                        case "__SystemUInt32":
                        case "__SystemInt64":
                        case "__SystemUInt64":
                        case "__SystemSingle":
                        case "__SystemDouble":
                        case "__SystemChar":
                        case "__SystemString": {
                                _args[i] = args[i] == null ? null : args[i].ToString();
                                break;
                            }
                        case "__UnityEngineVector2": {
                                if (args[i] != null) {
                                    var v = (Vector2)args[i];
                                    var s = $"{v[0]},{v[1]}";
                                }
                                break;
                            }
                        case "__UnityEngineVector3": {
                                if (args[i] != null) {
                                    var v = (Vector3)args[i];
                                    var s = $"{v[0]},{v[1]},{v[2]}";
                                }
                                break;
                            }
                        case "__UnityEngineVector4": {
                                if (args[i] != null) {
                                    var v = (Vector4)args[i];
                                    var s = $"{v[0]},{v[1]},{v[2]},{v[3]}";
                                }
                                break;
                            }
                        case "__UnityEngineQuaternion": {
                                if (args[i] != null) {
                                    var v = (Quaternion)args[i];
                                    var s = $"{v[0]},{v[1]},{v[2]},{v[3]}";
                                }
                                break;
                            }
                        case "__UnityEngineColor": {
                                if (args[i] != null) {
                                    var v = (Color)args[i];
                                    var s = $"{v[0]},{v[1]},{v[2]},{v[3]}";
                                }
                                break;
                            }
                        case "__UnityEngineColor32": {
                                if (args[i] != null) {
                                    var v = (Color32)args[i];
                                    var s = $"{v[0]},{v[1]},{v[2]},{v[3]}";
                                }
                                break;
                            }
                        case "__VRCSDKBaseVRCUrl": {
                                _urls[i] = (VRCUrl)args[i];
                                break;
                            }
                        default: {
                                Logger.LogError($"Unsupported argument type `{___5}`");
                                break;
                            }
                    }
                    // bug workaround.
                    // https://feedback.vrchat.com/open-beta/p/sdk-381-beta4-sendcustomnetworkevent-will-break-if-the-array-an-argument-contain
                    if (___5 == "__VRCSDKBaseVRCUrl") {
                        _args[i] = string.Empty;
                        if (args[i] == null) {
                            _urls[i] = VRCUrl.Empty;
                        }
                    }
                    else {
                        if (args[i] == null) {
                            _args[i] = string.Empty;
                        }
                        _urls[i] = VRCUrl.Empty;
                    }
                }
                SendCustomNetworkEvent(NetworkEventTarget.All, nameof(Relay), argsLength, idx, _channel, _args, _urls);
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

        [NetworkCallable]
        public void Relay(int argsLength, int idx, string channel, string[] args, VRCUrl[] urls) {
            var __3 = _3[idx];
            var __4 = _4[idx];
            var __5 = _5[idx];
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
                    var ___5 = __5[j];
                    switch (___5) {
                        case "__SystemBoolean": {
                                Boolean.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemByte": {
                                Byte.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemInt16": {
                                Int16.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemUInt16": {
                                UInt16.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemInt32": {
                                Int32.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemUInt32": {
                                UInt32.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemInt64": {
                                Int64.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemUInt64": {
                                UInt64.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemSingle": {
                                Single.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemDouble": {
                                Double.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemChar": {
                                Char.TryParse(args[j], out var value);
                                ___9.SetProgramVariable(__4[j], (object)value);
                                break;
                            }
                        case "__SystemString": {
                                ___9.SetProgramVariable(__4[j], (object)args[j]);
                                break;
                            }
                        case "__UnityEngineVector2": {
                                if (args[j] == null) {
                                    ___9.SetProgramVariable(__4[j], null);
                                }
                                else {
                                    var a = args[j].Split(',');
                                    var v = (Vector2)default;
                                    Single.TryParse(a[0], out var v0);
                                    v[0] = v0;
                                    Single.TryParse(a[1], out var v1);
                                    v[1] = v1;
                                    ___9.SetProgramVariable(__4[j], (object)v);
                                }
                                break;
                            }
                        case "__UnityEngineVector3": {
                                if (args[j] == null) {
                                    ___9.SetProgramVariable(__4[j], null);
                                }
                                else {
                                    var a = args[j].Split(',');
                                    var v = (Vector3)default;
                                    Single.TryParse(a[0], out var v0);
                                    v[0] = v0;
                                    Single.TryParse(a[1], out var v1);
                                    v[1] = v1;
                                    Single.TryParse(a[2], out var v2);
                                    v[2] = v2;
                                    ___9.SetProgramVariable(__4[j], (object)v);
                                }
                                break;
                            }
                        case "__UnityEngineVector4": {
                                if (args[j] == null) {
                                    ___9.SetProgramVariable(__4[j], null);
                                }
                                else {
                                    var a = args[j].Split(',');
                                    var v = (Vector4)default;
                                    Single.TryParse(a[0], out var v0);
                                    v[0] = v0;
                                    Single.TryParse(a[1], out var v1);
                                    v[1] = v1;
                                    Single.TryParse(a[2], out var v2);
                                    v[2] = v2;
                                    Single.TryParse(a[3], out var v3);
                                    v[3] = v3;
                                    ___9.SetProgramVariable(__4[j], (object)v);
                                }
                                break;
                            }
                        case "__UnityEngineQuaternion": {
                                if (args[j] == null) {
                                    ___9.SetProgramVariable(__4[j], null);
                                }
                                else {
                                    var a = args[j].Split(',');
                                    var v = (Quaternion)default;
                                    Single.TryParse(a[0], out var v0);
                                    v[0] = v0;
                                    Single.TryParse(a[1], out var v1);
                                    v[1] = v1;
                                    Single.TryParse(a[2], out var v2);
                                    v[2] = v2;
                                    Single.TryParse(a[3], out var v3);
                                    v[3] = v3;
                                    ___9.SetProgramVariable(__4[j], (object)v);
                                }
                                break;
                            }
                        case "__UnityEngineColor": {
                                if (args[j] == null) {
                                    ___9.SetProgramVariable(__4[j], null);
                                }
                                else {
                                    var a = args[j].Split(',');
                                    var v = (Color)default;
                                    Single.TryParse(a[0], out var v0);
                                    v[0] = v0;
                                    Single.TryParse(a[1], out var v1);
                                    v[1] = v1;
                                    Single.TryParse(a[2], out var v2);
                                    v[2] = v2;
                                    Single.TryParse(a[3], out var v3);
                                    v[3] = v3;
                                    ___9.SetProgramVariable(__4[j], (object)v);
                                }
                                break;
                            }
                        case "__UnityEngineColor32": {
                                if (args[j] == null) {
                                    ___9.SetProgramVariable(__4[j], null);
                                }
                                else {
                                    var a = args[j].Split(',');
                                    var v = (Color32)default;
                                    Byte.TryParse(a[0], out var v0);
                                    v[0] = v0;
                                    Byte.TryParse(a[1], out var v1);
                                    v[1] = v1;
                                    Byte.TryParse(a[2], out var v2);
                                    v[2] = v2;
                                    Byte.TryParse(a[3], out var v3);
                                    v[3] = v3;
                                    ___9.SetProgramVariable(__4[j], (object)v);
                                }
                                break;
                            }
                        case "__VRCSDKBaseVRCUrl": {
                                ___9.SetProgramVariable(__4[j], (object)urls[j]);
                                break;
                            }
                        default: {
                                Logger.LogError($"Unsupported argument type `{___5}`");
                                break;
                            }
                    }
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
