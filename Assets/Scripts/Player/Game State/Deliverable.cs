using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    public abstract class Deliverable : ScriptableObject
    {
        public abstract int AdjustedPrice { get; }
        public abstract string EmailAttachment ();
    }

    [Serializable, DataContract]
    public abstract class Deliverable<T> : Deliverable, IEquatable<Deliverable<T>> where T : Service
    {
        [DataMember]
        public T Service;

        public override int AdjustedPrice => Service.BasePrice;

        public bool Equals (Deliverable<T> other)
        {
            // base reference equality on Services (but not on this)
            return Service == other.Service;
        }

        public override string EmailAttachment ()
        {
            return $"Service: {Service.PrettyName}\nUnit Price: {AdjustedPrice} gp";
        }
    }
}
