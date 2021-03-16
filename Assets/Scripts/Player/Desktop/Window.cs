using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using crass;

namespace WitchOS
{
    public class Window : MonoBehaviour, IPointerDownHandler
    {
        // in order for particles (and potentially other effects) to be able to be layered between UI elements, we need to be able to set the sorting order of effects in between the elements we want to layer. therefore we create artificial "sorting planes" which are areas of the sorting order space divided by an increment of SORTING_PLANE_SIZE. each window has its own sorting plane, and effects can go above or below that as desired.
        public const int SORTING_PLANE_SIZE = 10;

        public static Window FocusedWindow { get; private set; }

        public bool Focused => FocusedWindow == this;

        public FileBase File { get; private set; }

        public string Title
        {
            get => TitleText.text;
            set => TitleText.text = value;
        }

        public Sprite Icon
        {
            get => IconImage.sprite;
            set => IconImage.sprite = value;
        }

        [Header("Data")]
        public bool Minimizable = true, Closable = true;
        public Minimizer Minimizer;

        public UnityEvent DidOpen, DidFocus;
        public IntEvent SortingPlaneDidChange;

        [Header("References")]
        public Canvas LocalCanvas;
        public TextMeshProUGUI TitleText;
        public Image IconImage;
        public Button MinimizeButton, CloseButton;

        public FileAssociationConfig FileAssociationConfig;
        public TimeState TimeState;

        TaskBarButton taskBarButton;

        int sortingPlane => transform.GetSiblingIndex() * SORTING_PLANE_SIZE;

        void Start ()
        {
            // check if buttons are enabled here and not in methods so that code can minimize the window regardless of user-interactability state

            MinimizeButton.onClick.AddListener
            (
                () => { if (Minimizable) Minimizer.Minimize(); }
            );

            CloseButton.onClick.AddListener
            (
                () => { if (Closable) Close(); }
            );

            DidOpen.Invoke();

            setSortingPlane();
        }

        void Update ()
        {
            MinimizeButton.interactable = Minimizable;
            CloseButton.interactable = Closable;

            if (LocalCanvas.sortingOrder != sortingPlane)
            {
                setSortingPlane();
            }
        }

        public void SetFile (FileBase file)
        {
            File = file;

            var metadata = FileAssociationConfig.GetMetadataForFile(File);
            var name = metadata.GetWindowName(File.Name);

            gameObject.name = name;
            Title = name;
            Icon = metadata.IconSmall;
        }

        public void SetTaskBarButton (TaskBarButton taskBarButton)
        {
            this.taskBarButton = taskBarButton;
            Minimizer.MinimizeTarget = taskBarButton.transform as RectTransform;
        }

        public void Close ()
        {
            TimeState.DayEnded.RemoveListener(Close);
            Destroy(gameObject);
            if (taskBarButton != null) Destroy(taskBarButton.gameObject);

            // in case it already isn't:
            CursorManager.Instance.CursorState = CursorState.Normal;
        }

        public void Focus ()
        {
            FocusedWindow = this;
            transform.SetAsLastSibling(); // bring to front
            DidFocus.Invoke();
        }

        public void OnPointerDown (PointerEventData eventData)
        {
            Focus();
        }

        void setSortingPlane ()
        {
            LocalCanvas.sortingOrder = sortingPlane;
            SortingPlaneDidChange.Invoke(sortingPlane);

        }
    }
}
