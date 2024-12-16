using HoshinoLabs.Sardinject.Udon;
using System;

namespace HoshinoLabs.Sardinal.Udon {
    [Serializable]
    public class Signal : ISerializable {
        Type topic;

        public Signal() {

        }

        public Signal(Type topic) {
            this.topic = topic;
        }

        public void Serialize(IDataWriter writer) {
#if !COMPILER_UDONSHARP && UNITY_EDITOR
            writer.WriteReference("", SardinalResolver.Resolve());
            writer.WriteString("", topic.FullName.ComputeHashMD5());
            writer.WriteNull("");
#endif
        }

        public void Deserialize(IDataReader reader) {
#if !COMPILER_UDONSHARP && UNITY_EDITOR
            reader.SkipEntry();
            reader.SkipEntry();
            reader.SkipEntry();
#endif
        }
    }

    [Serializable]
    public sealed class Signal<T> : Signal {
        public Signal()
            : base(typeof(T)) {

        }
    }
}
