using HoshinoLabs.Sardinject.Udon;
using System;
using UnityEngine;

namespace HoshinoLabs.Sardinal.Udon {
    [Serializable]
    public sealed class Signal<T> : ISerializable {
        [Inject, SerializeField, HideInInspector]
        Sardinal sardinal;

        public void Serialize(IDataWriter writer) {
            writer.WriteReference("", sardinal);
            writer.WriteString("", typeof(T).FullName.ComputeHashMD5());
            writer.WriteNull("");
        }

        public void Deserialize(IDataReader reader) {
            reader.SkipEntry();
            reader.SkipEntry();
            reader.SkipEntry();
        }
    }
}
