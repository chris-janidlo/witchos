using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BankTransaction
{
    public int InitialCurrency, DeltaCurrency;
    public string Description;

    [SerializeField]
    string dateString;
    public DateTime Date
    {
        get => DateTime.Parse(dateString);
        set => dateString = value.ToString();
    }
}
