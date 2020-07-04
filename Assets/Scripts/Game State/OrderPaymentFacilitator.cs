using UnityEngine;

namespace WitchOS
{
    public class OrderPaymentFacilitator : MonoBehaviour
    {
        public void OnOrderTurnedIn (Order order)
        {
            BankState.Instance.AddTransaction(order.InvoiceData.TotalPrice, $"invoice {order.InvoiceData.OrderNumber} payment");
        }
    }
}
