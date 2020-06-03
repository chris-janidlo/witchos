using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static SaveData<LooseSaveValues> LooseSaveData { get; private set; }
    public static SaveData<List<BankTransaction>> BankTransactionData { get; private set; }
    public static SaveData<List<Invoice>> EMailData { get; private set; }

    static List<SaveData> allDataObjects;

    static SaveManager ()
    {
        LooseSaveData = new SaveData<LooseSaveValues>
        (
            "looseData",
            new LooseSaveValues { Date = new DateTime(2000, 10, 13) }
        );

        BankTransactionData = new SaveData<List<BankTransaction>>
        (
            "bankTransactions",
            new List<BankTransaction>
            {
                new BankTransaction
                {
                    InitialCurrency = 0,
                    DeltaCurrency = 100,
                    Description = "Initial Deposit",
                    Date = new DateTime(2000, 10, 13)
                }
            }
        );

        EMailData = new SaveData<List<Invoice>>
        (
            "emailData",
            new List<Invoice>()
        );

        allDataObjects = new List<SaveData>
        {
            LooseSaveData, BankTransactionData, EMailData
        };
    }

    public static void SaveAllData ()
    {
        allDataObjects.ForEach(s => s.WriteDataToFile());
    }

    public static void DeleteAllSaveData ()
    {
        allDataObjects.ForEach(s => s.DeleteSaveFile());
    }
}

public struct LooseSaveValues
{
    public DateTime Date;
    // list of icon positions
}
