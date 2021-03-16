using UnityEngine;

namespace WitchOS
{
    public class OrderPaymentFacilitator : MonoBehaviour
    {
        public void OnOrderTurnedIn (Order order)
        {
            BankState.Instance.AddTransaction(order.Invoice.TotalPrice, $"invoice {order.Invoice.OrderNumber} payment");
        }
    }
}
