using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable, DataContract]
public struct BankTransaction
{
    [DataMember]
    public int InitialCurrency, DeltaCurrency;
    [DataMember]
    public string Description;

    [SerializeField, DataMember]
    string dateString;
    public DateTime Date
    {
        get => DateTime.Parse(dateString);
        set => dateString = value.ToString();
    }
}
