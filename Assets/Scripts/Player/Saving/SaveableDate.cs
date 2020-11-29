using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public class SaveableDate
    {
        [SerializeField]
        string dateString;

        [DataMember(IsRequired = true)]
        public DateTime Value
        {
            get => DateTime.Parse(dateString);
            set => dateString = value.ToString();
        }

        public SaveableDate (DateTime date)
        {
            Value = date;
        }
    }
}
