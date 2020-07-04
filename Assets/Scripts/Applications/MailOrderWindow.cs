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

        public CanvasGroup TurnOrderInButtonCanvasGroup;

        public OrderEvent OrderTurnedIn;
        public SpellDeliverableValueList SpellEther;

        Order order => message as Order;

        void Start ()
        {
            SetTurnInButtonState();
        }

        public void SetTurnInButtonState ()
        {
            bool buttonOn = orderIsReadyToBeTurnedIn();
            TurnOrderInButtonCanvasGroup.alpha = buttonOn ? 1 : 0;
            TurnOrderInButtonCanvasGroup.interactable = buttonOn;
        }

        public void TurnInOrder ()
        {
            order.State = OrderState.Completed;
            OrderTurnedIn.Raise(order);
            Window.Close();
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

        bool orderIsReadyToBeTurnedIn ()
        {
            if (order.State != OrderState.InProgress) return false;

            foreach (var request in order.InvoiceData.LineItems)
            {
                if (request is SpellDeliverable)
                {
                    if (!SpellEther.Any(spell => spell.Equals(request as SpellDeliverable)))
                        return false;
                }
                else
                {
                    // potion check would go here
                    throw new System.Exception("there's only one type of deliverable, how did the code get here");
                }
            }

            return true;
        }
    }
}
