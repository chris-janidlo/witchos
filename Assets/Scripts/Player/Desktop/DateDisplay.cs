using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

namespace WitchOS
{
    public class DateDisplay : MonoBehaviour
    {
        [TextArea]
        public string Format = "dddd MMMM dd";
        public TextMeshProUGUI Text;
        
        public TimeState TimeState;

        void Update ()
        {
            Text.text = TimeState.DateTime.ToString(Format, TimeState.CULTURE_INFO);
        }
    }
}
