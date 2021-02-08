using System;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public class Order : Email, IEquatable<Order>
    {
        [DataMember(IsRequired = true)]
        public Invoice Invoice;

        [DataMember(IsRequired = true)]
        public DateTime DueDate;

        [DataMember(IsRequired = true)]
        public OrderState State;

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
            return
                base.Equals(other) &&
                Invoice.Equals(other.Invoice) &&
                DueDate == other.DueDate;
        }
    }
}
