using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable]
    public class WindowMetadata
    {
        public const string FILENAME_PLACEHOLDER = "##file##";

        public enum NewWindowBehavior
        {
            FocusOldWindow, OpenOneWindowPerFile, AlwaysOpenNewWindow
        }

        [Header("Behavior")]
        public Window WindowPrefab;
        [Tooltip("Keep this null to use the default desktop icon configured per icon drawer. If this is not null, the value provided will be used instead.")]
        public DesktopIcon DesktopIconPrefabOverride;
        public NewWindowBehavior NewWindowMode;
        public bool AddButtonToTaskbar = true;

        [Header("Presentation")]
        public string NameTemplate = FILENAME_PLACEHOLDER;
        public Sprite IconLarge, IconSmall;

        public string GetWindowName (string filename)
        {
            return NameTemplate.Replace(FILENAME_PLACEHOLDER, filename);
        }
    }
}
