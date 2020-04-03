using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

// TODO: this is just mock-level stuff to prove out the mail application. need to sit down and actually design the backend mail system
public class MailState : Singleton<MailState>
{
    [Serializable]
    public class MailBag : BagRandomizer<EMail> {}

    public MailBag PossibleEMails;
    public Vector2Int NewEMailsPerDayRange;
    [Range(0, 1)]
    public float NewEMailPerHourChance;
    public int MaxEMails;

    public List<EMail> CurrentMessages;

    public int UnreadMessageCount => CurrentMessages.Where(m => !m.Read).Count();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    void Start ()
    {
        TimeState.Instance.DayStarted += resetInbox;
        TimeState.Instance.HourStarted += onHourStarted;

        SpellWatcher.Instance.SpellCast += onSpellCast;

        resetInbox();
    }

    void resetInbox ()
    {
        CurrentMessages = new List<EMail>();

        for (int i = 0; i < RandomExtra.Range(NewEMailsPerDayRange); i++)
        {
            CurrentMessages.Add(PossibleEMails.GetNext());
        }
    }

    void onHourStarted ()
    {
        if (CurrentMessages.Count < MaxEMails && RandomExtra.Chance(NewEMailPerHourChance))
        {
            CurrentMessages.Add(PossibleEMails.GetNext());
        }
    }

    void onSpellCast (Spell spell)
    {
        Debug.Log(spell);
		for (int i = CurrentMessages.Count - 1; i >= 0; i--)
        {
            var message = CurrentMessages[i];
            if (message.RequestedSpell == spell)
            {
                Alert.Instance.ShowMessage($"successfully completed order '{message.Subject}' from {message.SenderAddress}! now removing it from your inbox...");
                CurrentMessages.RemoveAt(i);
                message.Complete();
            }
        }
    }
}

[Serializable]
public class EMail
{
    public event Action Completed;

    public Spell RequestedSpell;
    public string SenderAddress, Subject;
    [TextArea]
    public string Body;

    public bool Read;

    public override int GetHashCode ()
    {
        return (SenderAddress + Subject + Body).GetHashCode();
    }

    public void Complete ()
    {
        Completed?.Invoke();
    }
}
