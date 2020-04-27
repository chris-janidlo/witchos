using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class MailState : Singleton<MailState>
{
    [Serializable]
    public class MailBag : BagRandomizer<Invoice> {}

    public MailBag PossibleEMails;
    public AnimationCurve EMailsToSpawnByDifficulty;
    public int CurrentDifficultyLevel; // the highest level of difficulty that has been completed in one day

    public int TasksCompleted, TotalTasks;

    public List<Invoice> CurrentMessages;

    public int UnreadMessageCount => CurrentMessages.Where(m => !m.Read).Count();

    int difficulty => (int) EMailsToSpawnByDifficulty.Evaluate(CurrentDifficultyLevel);

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
        if (TasksCompleted == difficulty)
        {
            CurrentDifficultyLevel++;
        }

        while (CurrentMessages.Count < difficulty)
        {
            CurrentMessages.Add(PossibleEMails.GetNext());
        }

        TasksCompleted = 0;
        TotalTasks = difficulty;
    }

    void onSpellCast (Spell spell)
    {
		for (int i = CurrentMessages.Count - 1; i >= 0; i--)
        {
            var message = CurrentMessages[i];
            if (message.RequestedSpell == spell)
            {
                Alert.Instance.ShowMessage($"successfully completed order '{message.EmailSubjectLine}' from {message.BuyerAddress}! now removing it from your inbox...");
                CurrentMessages.RemoveAt(i);
                message.Completed.Invoke();
                TasksCompleted++;
            }
        }
    }
}
