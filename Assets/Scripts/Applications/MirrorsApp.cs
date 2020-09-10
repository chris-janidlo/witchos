using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
    public class MirrorsApp : MonoBehaviour
    {
        public Button HelpButton;
        public Window HelpWindowPrefab;

        void Start ()
        {
            HelpButton.onClick.AddListener(() => WindowFactory.Instance.OpenWindow(HelpWindowPrefab, WindowFactory.Options.Singleton));
        }
    }
}
