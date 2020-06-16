using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
public abstract class Deliverable: ScriptableObject
{
    public abstract float AdjustedPrice { get; }
    public abstract string EmailAttachment ();
}

[Serializable, DataContract]
public abstract class Deliverable<T> : Deliverable where T : Service
{
    [DataMember]
    public T Service;

    public override float AdjustedPrice => Service.BasePrice;

    public override string EmailAttachment ()
    {
        return $"Service: {Service.PrettyName}\nUnit Price: {AdjustedPrice}gp";
    }
}
}
