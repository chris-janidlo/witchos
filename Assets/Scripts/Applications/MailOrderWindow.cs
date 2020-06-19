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
    [TextArea]
    public string DateFormat = "MM/dd";

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
            $"{base.makeContentText()}\n\n\nAttachment: Invoice #{order.InvoiceData.OrderNumber}\n{SEPARATOR}";

        if (order.DueDate.Date <= TimeState.FINAL_DATE)
            emailContent += $"\nDue {order.DueDate.ToString(DateFormat, TimeState.CULTURE_INFO)}";
        else
            emailContent += "\nNo Specified Due Date";

        emailContent += "\n\nRequested services:";

        foreach (Deliverable lineItem in order.InvoiceData.LineItems)
        {
            emailContent += $"\n\n{lineItem.EmailAttachment()}";
        }

        emailContent += $"\n\nTotal: {order.InvoiceData.TotalPrice} gp";

        return emailContent;
    }

    bool orderFulfilled ()
    {
        return order.InvoiceData.LineItems.All(li => SpellEther.Contains(li));
    }
}
}
