namespace HoshinoLabs.Sardinal.Udon {
    internal static class SignalIdExtensions {
        public static object Pack(this SignalId self) {
            if (self.GetTopic() == null) {
                return null;
            }
            return new[] {
                self.GetTopic().ComputeHashMD5(),
            };
        }

        public static SignalId UnPack(object obj) {
            return null;
        }
    }
}
