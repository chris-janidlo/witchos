using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace WitchOS
{
    public class DesktopIcon : MonoBehaviour
    {
        [Header("Data")]
        public Sprite Icon;
        public string Label;

        [Header("References")]
        public Image IconImage;
        public TextMeshProUGUI LabelText;

        void Update ()
        {
            IconImage.sprite = Icon;
            LabelText.text = Label;
        }
    }
}
