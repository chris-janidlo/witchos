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
        public Sprite Icon
        {
            set => IconImage.sprite = value;
        }

        public Image IconImage;
        public TextMeshProUGUI LabelText;

        public FileAssociationConfig FileAssociationConfig;

        FileBase _file;
        public FileBase File
        {
            get => _file;
            set
            {
                _file = value;
                onFileDataUpdated();
            }
        }

        void Update ()
        {
            File.GuiPosition = transform.localPosition;
        }

        public void OnClick ()
        {
            WindowFactory.Instance.OpenWindowWithFile(File);
        }

        void onFileDataUpdated ()
        {
            LabelText.text = File.Name;
            Icon = FileAssociationConfig.GetMetadataForFile(File).IconLarge;

            transform.localPosition = File.GuiPosition;
        }
    }
}
