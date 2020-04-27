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
    public int MaxEMails;
    public Vector2Int NewEMailsPerDayRange;

    public int TasksCompleted, TotalTasks;

    public List<Invoice> CurrentMessages;

    public int UnreadMessageCount => CurrentMessages.Where(m => !m.Read).Count();

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
        TasksCompleted = 0;

        int maxToAdd = RandomExtra.Range(NewEMailsPerDayRange);

        while (CurrentMessages.Count < MaxEMails && maxToAdd-- > 0)
        {
            CurrentMessages.Add(PossibleEMails.GetNext());
        }

        TotalTasks = CurrentMessages.Count;
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
