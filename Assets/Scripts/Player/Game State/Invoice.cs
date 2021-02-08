using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace WitchOS
{
    [Serializable, DataContract]
    public class Invoice : IEquatable<Invoice>
    {
        [DataMember(IsRequired = true)]
        public List<SpellDeliverable> LineItems;

        [DataMember(IsRequired = true)]
        public int FullDaysToComplete;

        public int TotalPrice => LineItems.Sum(d => d.AdjustedPrice);

        public int OrderNumber => Math.Abs(GetHashCode());

        public bool Equals (Invoice other)
        {
            return
                LineItems.SequenceEqual(other.LineItems) &&
                FullDaysToComplete == other.FullDaysToComplete;
        }
    }
}
