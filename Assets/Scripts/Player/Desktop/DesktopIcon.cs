using System;
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

        public DirectoryDrawer debug;

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
            try
            {
                if (hoveredDirectoryDrawer() is DirectoryDrawer d)
                {
                    d.AddIcon(this);
                }
                else
                {
                    throw new FilesystemException("can't put that icon there");
                }
            }
            catch (FilesystemException e)
            {
                // could play a sound here
                sendBackToOldPosition();

                Debug.Log(e.Message);
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

        DirectoryDrawer hoveredDirectoryDrawer ()
        {
            // todo: raycast for whatever directory drawer layer I end up creating
            return debug;
        }

        void sendBackToOldPosition ()
        {
            transform.SetParent(dragStartParent, false);
            transform.position = dragStartPosition;
        }
    }
}
