using System;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    [KnownType(typeof(Order))]
    public class Email : IEquatable<Email>
    {
        public EmailData EmailData;

        [DataMember(IsRequired = true)]
        private int serializedEmailDataID
        {
            get => SOLookupTable.Instance.GetID(EmailData);
            set => EmailData = SOLookupTable.Instance.GetSO(value) as EmailData;
        }

        // for times when the subject should be annotated (see: Order)
        public virtual string AnnotatedSubject => EmailData.SubjectLine;

        public bool Equals (Email other)
        {
            return EmailData == other.EmailData;
        }
    }
}
