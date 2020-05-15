using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class OrderState : Singleton<OrderState>
{
    [Serializable]
    public class InvoiceBag : BagRandomizer<Invoice> {}

    public InvoiceBag PossibleInvoices;
    public List<int> InvoicesToSpawnByDifficulty;
    public int CurrentDifficultyLevel; // the highest level of difficulty that has been completed in one day

    public int TasksCompleted, TotalTasks;

    public GenericInbox<Invoice> InvoiceInbox;

    int spawnCount => InvoicesToSpawnByDifficulty[CurrentDifficultyLevel];

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    void Start ()
    {
        SpellWatcher.Instance.SpellCast += onSpellCast;
    }

    public void StartDay ()
    {
        if (TasksCompleted == spawnCount && CurrentDifficultyLevel < InvoicesToSpawnByDifficulty.Count- 1)
        {
            CurrentDifficultyLevel++;
        }

        while (InvoiceInbox.Entries.Count < spawnCount)
        {
            InvoiceInbox.Add(PossibleInvoices.GetNext());
        }

        TasksCompleted = 0;
        TotalTasks = spawnCount;
    }

    void onSpellCast (Casting casting)
    {
        var match = InvoiceInbox.Entries.FirstOrDefault(e => e.Value.SpellRequest == casting);

        if (!match.Equals(default(GenericInbox<Invoice>.Entry)))
        {
            Alert.Instance.ShowMessageImmediately($"you completed an order!");
            match.Value.Completed.Invoke();
            InvoiceInbox.Remove(match.Value);
            TasksCompleted++;
        }
    }
}
