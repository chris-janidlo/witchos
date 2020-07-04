using System;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public class Order : Email, IEquatable<Order>
    {
        public InvoiceData InvoiceData;

        [DataMember(IsRequired = true)]
        public DateTime DueDate;

        [DataMember(IsRequired = true)]
        public OrderState State;

        [DataMember(IsRequired = true)]
        private int serializedInvoiceDataID
        {
            get => SOLookupTable.Instance.GetID(InvoiceData);
            set => InvoiceData = SOLookupTable.Instance.GetSO(value) as InvoiceData;
        }

        public override string AnnotatedSubject
        {
            get
            {
                string prefix = "";

                switch (State)
                {
                    case OrderState.Completed:
                        prefix = "(completed) ";
                        break;

                    case OrderState.Failed:
                        prefix = "(failed) ";
                        break;
                }

                return prefix + base.AnnotatedSubject;
            }
        }

        public bool Equals (Order other)
        {
            return base.Equals(other) && InvoiceData == other.InvoiceData && DueDate == other.DueDate;
        }
    }
}
