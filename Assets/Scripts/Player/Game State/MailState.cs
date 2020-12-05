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

        public IReadOnlyList<Entry> CurrentMailEntries => SaveData.Value.AsReadOnly();

        public int UnreadMessageCount => CurrentMailEntries.Where(m => !m.Read).Count();
        public int OrdersInProgress => CurrentMailEntries.Select(e => e.Contents).Where(e => (e as Order)?.State == OrderState.InProgress).Count();
        
        public MailSaveData SaveData;
        public SaveManager SaveManager;

        void Awake ()
        {
            SingletonOverwriteInstance(this);
            SaveManager.Register(SaveData);

            if (SaveData.Value == null)
            {
                SaveData.Value = new List<Entry>();
            }
        }

        public void AddEmail (Email email)
        {
            SaveData.Value.Add(new Entry { Contents = email, Read = false });
        }

        public void OnSpellCast (SpellDeliverable spell)
        {
            var orders = SaveData.Value
                .Where(e => e.Contents is Order)
                .Select(e => e.Contents as Order);

            var incompleteOrderInvoices = orders
                .Where(o => o.State == OrderState.InProgress)
                .Select(o => o.InvoiceData.Value);

            foreach (InvoiceData invoice in incompleteOrderInvoices)
            {
                var spells = invoice.LineItems.Where(li => li is SpellDeliverable).Select(li => li as SpellDeliverable).ToList();

                if (spells.Contains(spell))
                {
                    Alert.Instance.ShowMessage($"WitchWatch: spell {spells.IndexOf(spell) + 1} was cast for order #{invoice.OrderNumber}");
                }
            }
        }

        public void FailOverdueOrders ()
        {
            foreach (Order order in SaveData.Value.Select(entry => entry.Contents).Where(e => e is Order))
            {
                if (order.State == OrderState.InProgress && order.DueDate.Date <= TimeState.Instance.DateTime.Date)
                    order.State = OrderState.Failed;
            }
        }
    }
}
