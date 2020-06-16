using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
public class OrderGenerator : MonoBehaviour
{
    [Serializable]
    public class OrderBag : BagRandomizer<OrderData> {}

    public OrderBag PossibleOrders;
    public List<int> OrdersToSpawnByDifficulty;

    public int TasksCompletedToday;

    public int CurrentDifficultyLevel
    {
        get => SaveManager.LooseSaveData.Value.CurrentDifficultyLevel;
        set => SaveManager.LooseSaveData.Value.CurrentDifficultyLevel = value;
    }

    int spawnCount => OrdersToSpawnByDifficulty[CurrentDifficultyLevel];

    public void GenerateOrders ()
    {
        TasksCompletedToday = 0;

        while (MailState.Instance.CurrentMailEntries.Count < spawnCount)
        {
            MailState.Instance.AddEmail(PossibleOrders.GetNext().GenerateOrder());
        }
    }

    public void CountCompletedOrder ()
    {
        TasksCompletedToday++;

        if (TasksCompletedToday >= spawnCount && CurrentDifficultyLevel < OrdersToSpawnByDifficulty.Count- 1)
        {
            CurrentDifficultyLevel++;
        }
    }
}
}
