using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable, DataContract]
    public struct SaveableVector3
    {
        [DataMember(IsRequired = true)]
        public float x, y, z;

        public static implicit operator Vector3 (SaveableVector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static implicit operator SaveableVector3 (Vector3 v)
        {
            return new SaveableVector3 { x = v.x, y = v.y, z = v.z };
        }
    }
}
