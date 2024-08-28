using HoshinoLabs.Sardinal.Udon;
using System;
using System.Collections;
using System.Collections.Generic;
using UdonSharp.Serialization;
using UnityEngine;

namespace HoshinoLabs.Sardinal {
    internal sealed class SardinalSerializer : UdonSharp.Serialization.Serializer<ISardinal> {
        public SardinalSerializer(TypeSerializationMetadata typeMetadata)
            : base(typeMetadata) {

        }

        protected override Serializer MakeSerializer(TypeSerializationMetadata typeMetadata) {
            throw new NotImplementedException();
        }

        protected override bool HandlesTypeSerialization(TypeSerializationMetadata typeMetadata) {
            VerifyTypeCheckSanity();
            if (typeMetadata.cSharpType == ISardinal.ImplementationType) {
                SardinalBuilder.Build();
            }
            return false;
        }

        public override void Write(IValueStorage targetObject, in ISardinal sourceObject) {
            throw new NotImplementedException();
        }

        public override void Read(ref ISardinal targetObject, IValueStorage sourceObject) {
            throw new NotImplementedException();
        }

        public override Type GetUdonStorageType() {
            throw new NotImplementedException();
        }
    }
}
