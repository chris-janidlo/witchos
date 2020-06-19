using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityAtoms;
using crass;

namespace WitchOS
{
public class MailState : Singleton<MailState>
{
    [DataContract]
    public class Entry
    {
        [DataMember(IsRequired = true)]
        public bool Read;

        [DataMember(IsRequired = true)]
        public Email Contents;
    }

    SaveData<List<Entry>> messageData;
    public IReadOnlyList<Entry> CurrentMailEntries => messageData.Value.AsReadOnly();

    public int UnreadMessageCount => CurrentMailEntries.Where(m => !m.Read).Count();

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

    public void AddEmail (Email email)
    {
        messageData.Value.Add(new Entry { Contents = email, Read = false });
    }

    public void OnSpellCast (SpellDeliverable spell)
    {
		foreach (Entry entry in messageData.Value)
        {
            if (!(entry.Contents is Order)) continue;

            var invoice = (entry.Contents as Order).InvoiceData;

            // only search on SpellDeliverables, cast as such, so that the proper Equals method is called
            if (invoice.LineItems.Where(li => li is SpellDeliverable).Select(li => li as SpellDeliverable).Contains(spell))
            {
                Alert.Instance.ShowMessage($"WitchWatch: spell {invoice.LineItems.IndexOf(spell) + 1} was cast for order #{invoice.OrderNumber}");
            }
        }
    }

    public void DeleteOverdueOrders ()
    {
        messageData.Value.RemoveAll(e =>
        {
            if (!(e.Contents is Order)) return false;

            return (e.Contents as Order).DueDate.Date <= TimeState.Instance.DateTime.Date;
        });
    }
}
}
