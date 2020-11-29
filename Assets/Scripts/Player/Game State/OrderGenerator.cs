﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class OrderGenerator : MonoBehaviour
    {
        [Serializable]
        public class OrderBag : BagRandomizer<OrderData> { }

        public OrderBag PossibleOrders;
        public List<int> OrdersToSpawnByDifficulty;

        public int TasksCompletedToday;

        public IntSaveData DifficultyLevelSaveData;
        public SaveManager SaveManager;

        public int CurrentDifficultyLevel
        {
            get => DifficultyLevelSaveData.Value;
            set => DifficultyLevelSaveData.Value = value;
        }

        int spawnCount => OrdersToSpawnByDifficulty[CurrentDifficultyLevel];

        void Start ()
        {
            SaveManager.Register(DifficultyLevelSaveData);
        }

        public void GenerateOrders ()
        {
            TasksCompletedToday = 0;

            while (MailState.Instance.OrdersInProgress < spawnCount)
            {
                MailState.Instance.AddEmail(PossibleOrders.GetNext().GenerateOrder());
            }
        }

        public void CountCompletedOrder ()
        {
            TasksCompletedToday++;

            if (TasksCompletedToday >= spawnCount && CurrentDifficultyLevel < OrdersToSpawnByDifficulty.Count - 1)
            {
                CurrentDifficultyLevel++;
            }
        }
    }
}
