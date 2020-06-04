using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class BankState : Singleton<BankState>
{
    public int CurrentBalance => Transactions.Sum(t => t.DeltaCurrency);

    public string InsufficientFundsAlertMessage;
    public BankTransaction InitialTransaction;

    SaveData<List<BankTransaction>> transactionData;
    public IReadOnlyList<BankTransaction> Transactions => transactionData.Value.AsReadOnly();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    void Start ()
    {
        transactionData = new SaveData<List<BankTransaction>>
        (
            "bankTransactionData",
            new List<BankTransaction> { InitialTransaction }
        );

        SaveManager.RegisterSaveDataObject(transactionData);
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

        transactionData.Value.Add(transaction);

        return true;
    }

    public bool HaveEnoughMoney (int deltaCurrency)
    {
        if (deltaCurrency >= 0) return true;
        else return CurrentBalance + deltaCurrency >= 0;
    }
}
