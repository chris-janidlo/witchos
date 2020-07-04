using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WitchOS
{
    [CreateAssetMenu(fileName = "NewInvoice.asset", menuName = "WitchOS/Invoice")]
    public class InvoiceData : ScriptableObject
    {
        public List<Deliverable> LineItems;
        public int FullDaysToComplete;

        public float TotalPrice => LineItems.Sum(d => d.AdjustedPrice);

        public int OrderNumber => Math.Abs(GetHashCode());
    }
}
