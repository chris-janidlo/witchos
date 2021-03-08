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
        public class OrderBag : BagRandomizer<EmailBlueprint> { }

        public OrderBag PossibleOrders;
        public List<int> OrdersToSpawnByDifficulty;

        public int TasksCompletedToday;

        public IntSaveData DifficultyLevelSaveData;

        public TimeState TimeState;
        public SaveManager SaveManager;

        public int CurrentDifficultyLevel
        {
            get => DifficultyLevelSaveData.Value;
            set => DifficultyLevelSaveData.Value = value;
        }

        int spawnCount => OrdersToSpawnByDifficulty[CurrentDifficultyLevel];

        void Awake ()
        {
            SaveManager.Register(DifficultyLevelSaveData);
            TimeState.DayStarted.AddListener(generateOrders);
        }

        public void CountCompletedOrder ()
        {
            TasksCompletedToday++;

            if (TasksCompletedToday >= spawnCount && CurrentDifficultyLevel < OrdersToSpawnByDifficulty.Count - 1)
            {
                CurrentDifficultyLevel++;
            }
        }

        void generateOrders ()
        {
            TasksCompletedToday = 0;

            while (MailState.Instance.OrdersInProgress < spawnCount)
            {
                MailState.Instance.AddEmail(PossibleOrders.GetNext().GenerateEmail());
            }
        }
    }
}
