using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms.WitchOS;
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

        Order order;

        void Start ()
        {
            order = Window.File.GetData<Order>();

            Window.Title = order.AnnotatedSubject;
            ContentText.text = makeOrderText(order);

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

        string makeOrderText (Order order)
        {
            string emailContent =
                $"{makeEmailText(order)}\n\n\nAttachment: Invoice #{order.Invoice.OrderNumber}\n{SEPARATOR}";

            if (order.DueDate.Date <= TimeState.FINAL_DATE)
                emailContent += $"\nDue {order.DueDate.ToString(DateFormat, TimeState.CULTURE_INFO)}";
            else
                emailContent += "\nNo Specified Due Date";

            emailContent += "\n\nRequested services:";

            foreach (Deliverable lineItem in order.Invoice.LineItems)
            {
                emailContent += $"\n\n{lineItem.EmailAttachment()}";
            }

            emailContent += $"\n\nTotal: {order.Invoice.TotalPrice} gp";

            return emailContent;
        }

        bool orderIsReadyToBeTurnedIn ()
        {
            if (order.State != OrderState.InProgress) return false;

            foreach (var request in order.Invoice.LineItems)
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
