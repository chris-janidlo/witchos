using System;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    [KnownType(typeof(Order))]
    public class Email : IEquatable<Email>
    {
        [Serializable, DataContract]
        public class SaveableEmailDataReference : SaveableScriptableObjectReference<EmailData> { }

        [DataMember(IsRequired = true)]
        public SaveableEmailDataReference EmailData;

        // for times when the subject should be annotated (see: Order)
        public virtual string AnnotatedSubject => EmailData.Value.SubjectLine;

        public Email (EmailData emailData)
        {
            if (EmailData == null)
            {
                EmailData = new SaveableEmailDataReference();
            }

            EmailData.Value = emailData;
        }

        public bool Equals (Email other)
        {
            return EmailData.Value == other.EmailData.Value;
        }
    }
}
