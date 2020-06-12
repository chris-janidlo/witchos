using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
[Serializable, DataContract]
public abstract class Deliverable<T> where T : Service
{
    [DataMember]
    public T Service;

    public virtual float AdjustedPrice => Service.BasePrice;
}
}
