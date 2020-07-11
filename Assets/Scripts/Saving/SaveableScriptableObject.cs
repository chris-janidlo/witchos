using System;
using System.Collections;
using System.Collections.Generic;
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
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "this property only exists so that the get/set code is called by the serializer. beyond that, it shouldn't be used anywhere - instead, Reference should always be used directly")]
        private int serializedLookupID
        {
            get => SOLookupTable.Instance.GetID(Value);
            set => Value = SOLookupTable.Instance.GetSO(value) as T;
        }
    }
}
