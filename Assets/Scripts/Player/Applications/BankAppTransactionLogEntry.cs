using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WitchOS
{
    public class BankAppTransactionLogEntry : MonoBehaviour
    {
        public TextMeshProUGUI DateText, DescriptionText, AmountText, BalanceText;

        public void SetTransaction (BankTransaction transaction)
        {
            DateText.text = transaction.Date.ToString("d", DateTimeFormatInfo.InvariantInfo);
            DescriptionText.text = transaction.Description;
            AmountText.text = transaction.DeltaCurrency.ToString("+#;-#;0"); // from https://stackoverflow.com/a/348242/5931898
            BalanceText.text = (transaction.InitialCurrency + transaction.DeltaCurrency).ToString();
        }
    }
}
