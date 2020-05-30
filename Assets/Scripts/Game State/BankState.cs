using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class BankState : Singleton<BankState>
{
    public int CurrentBalance => transactions.Sum(t => t.DeltaCurrency);

    public string InsufficientFundsAlertMessage;

    [SerializeField]
    List<BankTransaction> transactions = new List<BankTransaction>();
    public IReadOnlyList<BankTransaction> Transactions => transactions.AsReadOnly();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public bool AddTransaction (int deltaCurrency, string description, bool autoAlert = true)
    {
        if (!HaveEnoughMoney(deltaCurrency))
        {
            if (autoAlert) Alert.Instance.ShowMessageImmediately(InsufficientFundsAlertMessage);
            return false;
        }

        BankTransaction transaction = new BankTransaction
        {
            InitialCurrency = CurrentBalance,
            DeltaCurrency = deltaCurrency,
            Description = description,
            Date = TimeState.Instance.DateTime.Date
        };

        transactions.Add(transaction);

        return true;
    }

    public bool HaveEnoughMoney (int deltaCurrency)
    {
        if (deltaCurrency >= 0) return true;
        else return CurrentBalance + deltaCurrency < 0;
    }
}
