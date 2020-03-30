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
    public class PlaceholderMailMessage
    {
        public string SenderAddress, Subject, Body;
        public bool Read;

        public override int GetHashCode ()
        {
            return (SenderAddress + Subject + Body).GetHashCode();
        }
    }

    public List<PlaceholderMailMessage> Messages;

    public int UnreadMessageCount => Messages.Where(m => !m.Read).Count();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }
}
