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
    private int serializedInvoiceDataID
    {
        get => SOLookupTable.Instance.GetID(InvoiceData);
        set => InvoiceData = SOLookupTable.Instance.GetSO(value) as InvoiceData;
    }

	public bool Equals (Order other)
	{
        return base.Equals(other) && InvoiceData == other.InvoiceData && DueDate == other.DueDate;
	}
}
}
