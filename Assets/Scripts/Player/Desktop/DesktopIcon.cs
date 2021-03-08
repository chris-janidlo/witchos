﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityAtoms.BaseAtoms;
using TMPro;

namespace WitchOS
{
    public class DesktopIcon : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public Sprite Icon
        {
            set => IconImage.sprite = value;
        }

        public Image IconImage;
        public TextMeshProUGUI LabelText;

        public GameObjectEvent DesktopIconBeganDragging;
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

        Vector3 dragStartPosition;
        Transform dragStartParent;

        void Update ()
        {
            File.GuiPosition = transform.localPosition;
        }

        public void OnBeginDrag (PointerEventData eventData)
        {
            dragStartPosition = transform.position;
            dragStartParent = transform.parent;

            DesktopIconBeganDragging.Raise(gameObject);
        }

        public void OnEndDrag (PointerEventData eventData)
        {
            if (false) // check for desktop icon renderer under mouse cursor
            {
                // add this icon to that desktop icon renderer
            }
            else
            {
                transform.SetParent(dragStartParent, false);
                transform.position = dragStartPosition;
            }
        }

        public void OnClick ()
        {
            WindowFactory.Instance.OpenWindowWithFile(File);
        }

        void onFileDataUpdated ()
        {
            var metadata = FileAssociationConfig.GetMetadataForFile(File);

            if (metadata == null)
            {
                throw new InvalidOperationException($"no association set up for {File.GetType().FullName}");
            }

            LabelText.text = File.Name;
            Icon = metadata.IconLarge;

            transform.localPosition = File.GuiPosition;
        }
    }
}
