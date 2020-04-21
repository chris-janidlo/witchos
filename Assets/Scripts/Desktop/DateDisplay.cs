using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class DateDisplay : MonoBehaviour
{
    [TextArea]
    public string Format = "dddd MMMM dd";
    public TextMeshProUGUI Text;

    void Update ()
    {
        Text.text = TimeState.Instance.DateTime.ToString(Format, CultureInfo.CreateSpecificCulture("en-US"));
    }
}
