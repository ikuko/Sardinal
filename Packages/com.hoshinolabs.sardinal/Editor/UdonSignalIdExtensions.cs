namespace HoshinoLabs.Sardinal {
    internal static class UdonSignalIdExtensions {
        public static object Pack(this Udon.SignalId self) {
            if (self.GetTopic() == null) {
                return null;
            }
            return new[] {
                self.GetTopic().ComputeHashMD5(),
            };
        }

        public static Udon.SignalId UnPack(object obj) {
            return null;
        }
    }
}
