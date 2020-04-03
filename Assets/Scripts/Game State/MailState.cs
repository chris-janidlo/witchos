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
}

[Serializable]
public class EMail
{
    public Spell RequestedSpell;
    public string SenderAddress, Subject;
    [TextArea]
    public string Body;

    public bool Read;

    public override int GetHashCode ()
    {
        return (SenderAddress + Subject + Body).GetHashCode();
    }
}
