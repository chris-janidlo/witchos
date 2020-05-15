using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class MailState : Singleton<MailState>
{
    public GenericInbox<EMail> Inbox = new GenericInbox<EMail>();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }
}
