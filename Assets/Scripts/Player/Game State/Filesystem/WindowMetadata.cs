using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class WindowMetadata
    {
        public const string FILENAME_PLACEHOLDER = "##file##";

        public enum NewWindowBehavior
        {
            FocusOldWindow, OpenNewWindowPerFile, AlwaysOpenNewWindow
        }

        [Header("Behavior")]
        public Window WindowPrefab;
        public NewWindowBehavior NewWindowMode;
        public bool AddButtonToTaskbar = true;

        [Header("Presentation")]
        public Sprite Icon;
        public string NameTemplate;

        public string GetWindowName (string filename)
        {
            return NameTemplate.Replace(FILENAME_PLACEHOLDER, filename);
        }
    }
}
