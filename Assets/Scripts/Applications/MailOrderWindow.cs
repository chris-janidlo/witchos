using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;
using TMPro;

namespace WitchOS
{
public class MailOrderWindow : MailEmailWindow
{
    public Button TurnOrderInButton;

    public OrderEvent OrderTurnedIn;
    public SpellDeliverableValueList SpellEther;

    Order order => message as Order;

    void Update ()
    {
        TurnOrderInButton.gameObject.SetActive(orderFulfilled());
    }

    // hook this up to the button
    public void TurnInOrder ()
    {
        OrderTurnedIn.Raise(this.message as Order);
    }

    protected override string makeContentText ()
    {
        string emailContent =
            $@"{base.makeContentText()}
            {SEPARATOR}
            Invoice #{order.InvoiceData.OrderNumber}
            {SEPARATOR}
            Requested services:";

        foreach (Deliverable lineItem in order.InvoiceData.LineItems)
        {
            emailContent += $"\n{SEPARATOR}\n{lineItem.EmailAttachment()}";
        }

        emailContent += $"\n{SEPARATOR}\nTotal: {order.InvoiceData.TotalPrice}";

        return emailContent;
    }

    bool orderFulfilled ()
    {
        return order.InvoiceData.LineItems.All(li => SpellEther.Contains(li));
    }
}
}
