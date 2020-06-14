using System;
using System.Runtime.Serialization;

namespace WitchOS
{
[Serializable, DataContract]
public class Email : IEquatable<Email>
{
    public EmailData EmailData;

    [DataMember(IsRequired = true)]
    private int serializedEmailDataID
    {
        get => SOLookupTable.Instance.GetID(EmailData);
        set => EmailData = SOLookupTable.Instance.GetSO(value) as EmailData;
    }

	public bool Equals (Email other)
	{
		return EmailData == other.EmailData;
	}
}
}
