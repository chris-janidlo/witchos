using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
[CreateAssetMenu(fileName = "NewOrder.asset", menuName = "WitchOS/Order")]
public class OrderData : ScriptableObject
{
    [Serializable]
    public class EmailBag : BagRandomizer<EmailData> {}

    [Serializable]
    public class InvoiceBag : BagRandomizer<InvoiceData> {}

    public EmailBag PossibleEmails;
    public InvoiceBag PossibleInvoices;

    public Order GenerateOrder ()
    {
        InvoiceData invoiceData = PossibleInvoices.GetNext();

        DateTime dueDate = invoiceData.FullDaysToComplete < 0
            ? TimeState.FINAL_DATE.AddDays(7)
            : TimeState.Instance.AddDaysToToday(invoiceData.FullDaysToComplete);

        return new Order
        {
            EmailData = PossibleEmails.GetNext(),
            InvoiceData = invoiceData,
            DueDate = dueDate
        };
    }
}
}
