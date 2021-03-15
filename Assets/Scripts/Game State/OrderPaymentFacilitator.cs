using UnityEngine;

namespace WitchOS
{
    public class OrderPaymentFacilitator : MonoBehaviour
    {
        public void OnOrderTurnedIn (Order order)
        {
            BankState.Instance.AddTransaction(order.InvoiceData.Value.TotalPrice, $"invoice {order.InvoiceData.Value.OrderNumber} payment");
        }
    }
}
