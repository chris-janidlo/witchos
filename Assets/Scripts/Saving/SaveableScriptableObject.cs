using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class BaseSaveableScriptableObjectReference { }

    [DataContract]
    public class SaveableScriptableObjectReference<T> : BaseSaveableScriptableObjectReference
        where T : ScriptableObject
    {
        public T Value;

        [DataMember(IsRequired = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "this property only exists so that the get/set code is called by the serializer. beyond that, it shouldn't be used anywhere - instead, Value should always be used directly")]
        private string serializedPath
        {
            // FIXME - these exceptions aren't handled in any graceful way
            get => SOLookupTable.Instance.GetPath(Value) ?? throw new SerializationException($"asset {Value} is not in the lookup table");
            set => Value = SOLookupTable.Instance.GetAsset<T>(value) ?? throw new SerializationException($"path '{value}' is not in the lookup table");
        }
    }
}
