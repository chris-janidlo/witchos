using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public class TextPDF
    {
        [DataMember(IsRequired = true)]
        public string Title;

        [TextArea(5, 100)]
        [DataMember(IsRequired = true)]
        public List<string> Pages;
    }
}
