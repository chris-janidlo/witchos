using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Email Blueprint", fileName = "newEmailBlueprint.asset")]
    public class EmailBlueprint : ScriptableObject
    {
        [Serializable]
        public class EmailDataBag : BagRandomizer<Email> { }

        [Serializable]
        public class InvoiceBag : BagRandomizer<Invoice> { }

        public EmailDataBag PossibleEmails;
        public InvoiceBag PossibleInvoices;
        
        public TimeState TimeState;

        void Awake ()
        {
            if (PossibleEmails == null)
            {
                PossibleEmails = new EmailDataBag { Items = new List<Email>() };
            }

            OnValidate();
        }

        void OnValidate ()
        {
            if (PossibleEmails.Items.Count < 1)
            {
                PossibleEmails.Items.Add(new Email());
            }
        }

        public Email GenerateEmail ()
        {
            Email result = PossibleEmails.GetNext();

            if (PossibleInvoices.Items.Count > 0) result = generateOrderFromEmail(result);

            return result;
        }

        Order generateOrderFromEmail (Email email)
        {
            var invoice = PossibleInvoices.GetNext();

            DateTime dueDate = invoice.FullDaysToComplete < 0
                ? TimeState.FinalDate.AddDays(7)
                : TimeState.AddDaysToToday(invoice.FullDaysToComplete);

            return new Order
            {
                Invoice = invoice,
                DueDate = dueDate,
                SenderAddress = email.SenderAddress,
                SubjectLine = email.SubjectLine,
                Body = email.Body
            };
        }
    }
}
