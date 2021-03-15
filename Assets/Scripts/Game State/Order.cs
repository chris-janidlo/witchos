using System;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public class Order : Email, IEquatable<Order>
    {
        [Serializable, DataContract]
        public class SaveableInvoiceDataReference : SaveableScriptableObjectReference<InvoiceData> { }

        [DataMember(IsRequired = true)]
        public SaveableInvoiceDataReference InvoiceData;

        [DataMember(IsRequired = true)]
        public DateTime DueDate;

        [DataMember(IsRequired = true)]
        public OrderState State;

        public Order (EmailData emailData, InvoiceData invoiceData, DateTime dueDate) : base(emailData)
        {
            if (InvoiceData == null)
            {
                InvoiceData = new SaveableInvoiceDataReference();
            }

            InvoiceData.Value = invoiceData;
            DueDate = dueDate;
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
            return base.Equals(other) && InvoiceData.Value == other.InvoiceData.Value && DueDate == other.DueDate;
        }
    }
}
