using UdonSharp;

namespace HoshinoLabs.Sardinal.Udon {
    public static class SignalExtensions {
        public static Signal WithChannel(this Signal self, object channel) {
            var _self = (object[])(object)self;
            _self[2] = channel;
            return (Signal)(object)_self;
        }

        public static void Publish(this Signal self) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel);
        }

        public static void Publish(this Signal self, object arg0) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0);
        }

        public static void Publish(this Signal self, object arg0, object arg1) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        public static void Publish(this Signal self, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6, object arg7, object arg8, object arg9, object arg10, object arg11, object arg12, object arg13, object arg14, object arg15) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Publish($"{_topic}.", _channel, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }

        public static void Subscribe(this Signal self, UdonSharpBehaviour subscriber) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            var _channel = _self[2];
            _sardinal.Subscribe($"{_topic}.", _channel, subscriber);
        }

        public static void Unsubscribe(this Signal self, UdonSharpBehaviour subscriber) {
            if (self == null) {
                Logger.LogError("Attempting to use an invalid signal id");
                return;
            }
            var _self = (object[])(object)self;
            var _sardinal = (SardinalShim)_self[0];
            var _topic = (string)_self[1];
            _sardinal.Unsubscribe($"{_topic}.", subscriber);
        }
    }
}
