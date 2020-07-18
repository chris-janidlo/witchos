using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class BankState : Singleton<BankState>
    {
        public long CurrentBalance => Transactions.Sum(t => t.DeltaCurrency);

        public long MaximumBalance;

        public string InsufficientFundsAlertMessage, CappedBalanceAlertMessage;
        public BankTransaction InitialTransaction;

        SaveData<List<BankTransaction>> transactionData;
        public IReadOnlyList<BankTransaction> Transactions => transactionData.Value.AsReadOnly();

        void Awake ()
        {
            SingletonOverwriteInstance(this);

            transactionData = new SaveData<List<BankTransaction>>
            (
                "bankTransactionData",
                () => new List<BankTransaction> { InitialTransaction }
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

            long cappedDelta = Math.Min(deltaCurrency, MaximumBalance - CurrentBalance);

            if (cappedDelta != deltaCurrency)
            {
                if (cappedDelta < 0) throw new InvalidOperationException($"somehow CurrentBalance ({CurrentBalance}) got bigger than the MaximumBalance ({MaximumBalance})");
                if (autoAlert) Alert.Instance.ShowMessageImmediately(CappedBalanceAlertMessage);
                if (cappedDelta == 0) return false;
            }

            BankTransaction transaction = new BankTransaction
            {
                InitialCurrency = CurrentBalance,
                DeltaCurrency = cappedDelta,
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
}
