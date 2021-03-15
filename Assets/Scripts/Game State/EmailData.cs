using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(fileName = "NewEmail.asset", menuName = "WitchOS/Email")]
    public class EmailData : ScriptableObject
    {
        public string SenderAddress, SubjectLine;
        [TextArea(5, 100)]
        public string Body;
    }
}
