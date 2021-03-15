using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable, DataContract]
    [KnownType(typeof(Order))]
    public class Email : IEquatable<Email>
    {
        [DataMember(IsRequired = true)]
        public string SubjectLine, SenderAddress;

        [TextArea(5, 100)]
        [DataMember(IsRequired = true)]
        public string Body;

        // for times when the subject should be annotated (see: Order)
        public virtual string AnnotatedSubject => SubjectLine;

        public bool Equals (Email other)
        {
            return
                SenderAddress == other.SenderAddress &&
                SubjectLine == other.SubjectLine &&
                Body == other.Body;
        }
    }
}
