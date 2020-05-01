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
    public List<int> EMailsToSpawnByDifficulty;
    public int CurrentDifficultyLevel; // the highest level of difficulty that has been completed in one day

    public int TasksCompleted, TotalTasks;

    public List<Invoice> CurrentMessages;

    public int UnreadMessageCount => CurrentMessages.Where(m => !m.Read).Count();

    int spawnCount => EMailsToSpawnByDifficulty[CurrentDifficultyLevel];

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
        if (TasksCompleted == spawnCount)
        {
            CurrentDifficultyLevel++;
        }

        while (CurrentMessages.Count < spawnCount)
        {
            // kind of a kludge right now. all of the emails we currently have are repeatable filler emails that the player can and will see more than once. we want to preserve the unread state of the email across in-game days and across mail app sessions, and the only real way to do that is by storing the read state in the email object itself. that said, we don't want to store it on the ScriptableObject instance, since that preserves the read state across repeats of the same email, which is totally undesired behavior. so we create a copy of the email to add and add the copy. this is definitely not how we want to handle conversations or other one-off emails in the future
            var newMessageClone = Instantiate(PossibleEMails.GetNext());
            CurrentMessages.Add(newMessageClone);
        }

        TasksCompleted = 0;
        TotalTasks = spawnCount;
    }

    void onSpellCast (Spell spell)
    {
		for (int i = CurrentMessages.Count - 1; i >= 0; i--)
        {
            var message = CurrentMessages[i];
            if (message.RequestedSpell == spell)
            {
                Alert.Instance.ShowMessage($"you completed an order! it's been removed from your inbox.");
                CurrentMessages.RemoveAt(i);
                message.Completed.Invoke();
                TasksCompleted++;
            }
        }
    }
}
