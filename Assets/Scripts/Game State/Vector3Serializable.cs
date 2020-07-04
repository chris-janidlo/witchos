using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [DataContract]
    public struct Vector3Serializable
    {
        [DataMember(IsRequired = true)]
        public float x, y, z;

        public static implicit operator Vector3 (Vector3Serializable v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static implicit operator Vector3Serializable (Vector3 v)
        {
            return new Vector3Serializable { x = v.x, y = v.y, z = v.z };
        }
    }
}
