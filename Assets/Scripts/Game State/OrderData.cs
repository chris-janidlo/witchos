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
        return new Order
        {
            EmailData = PossibleEmails.GetNext(),
            InvoiceData = PossibleInvoices.GetNext()
        };
    }
}
}
