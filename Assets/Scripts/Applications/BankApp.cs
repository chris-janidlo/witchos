using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankApp : MonoBehaviour
{
    public BankAppTransactionLogEntry LogEntryPrefab;
    public VerticalLayoutGroup EntryLog;
    public TextMeshProUGUI BalanceDisplay;

    int entriesInLog;

    void Update ()
    {
        BalanceDisplay.text = BankState.Instance.CurrentBalance.ToString();

        if (entriesInLog != BankState.Instance.Transactions.Count)
            populateLog();
    }

    void populateLog ()
    {
        entriesInLog = BankState.Instance.Transactions.Count;

        foreach (Transform child in EntryLog.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (BankTransaction transaction in BankState.Instance.Transactions.Reverse())
        {
            Instantiate(LogEntryPrefab, EntryLog.transform).SetTransaction(transaction);
        }
    }
}
