using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using crass;

public class MailState : Singleton<MailState>
{
    [Serializable]
    public class MailBag : BagRandomizer<Invoice> {}

    [DataContract]
    public class Entry
    {
        [DataMember]
        public bool Read;

        public Invoice Contents;

        [DataMember]
        private int serializedContentsID
        {
            get => SOLookupTable.Instance.GetID(Contents);
            set => Contents = SOLookupTable.Instance.GetSO(value) as Invoice;
        }
    }

    public MailBag PossibleEMails;
    public List<int> EMailsToSpawnByDifficulty;

    public int CurrentDifficultyLevel
    {
        get => SaveManager.LooseSaveData.Value.CurrentDifficultyLevel;
        set => SaveManager.LooseSaveData.Value.CurrentDifficultyLevel = value;
    }

    public int TasksCompleted, TotalTasks;

    SaveData<List<Entry>> messageData;
    public IReadOnlyList<Entry> CurrentMailEntries => messageData.Value.AsReadOnly();

    public int UnreadMessageCount => CurrentMailEntries.Where(m => !m.Read).Count();

    int spawnCount => EMailsToSpawnByDifficulty[CurrentDifficultyLevel];

    void Awake ()
    {
        SingletonOverwriteInstance(this);

        messageData = new SaveData<List<Entry>>
        (
            "emailData",
            new List<Entry>()
        );

        SaveManager.RegisterSaveDataObject(messageData);
    }

    void Start ()
    {
        SpellWatcher.Instance.SpellCast += onSpellCast;
    }

    public void StartDay ()
    {
        if (TasksCompleted == spawnCount && CurrentDifficultyLevel < EMailsToSpawnByDifficulty.Count- 1)
        {
            CurrentDifficultyLevel++;
        }

        while (CurrentMailEntries.Count < spawnCount)
        {
            var newMessage = PossibleEMails.GetNext();
            messageData.Value.Add(new Entry { Read = false, Contents = newMessage });
        }

        TasksCompleted = 0;
        TotalTasks = spawnCount;
    }

    void onSpellCast (Casting casting)
    {
		for (int i = messageData.Value.Count - 1; i >= 0; i--)
        {
            var message = messageData.Value[i].Contents;
            if (message.SpellRequest == casting)
            {
                Alert.Instance.ShowMessage($"you completed an order! it's been removed from your inbox.");
                messageData.Value.RemoveAt(i);
                message.Completed.Invoke();
                TasksCompleted++;
            }
        }
    }
}
