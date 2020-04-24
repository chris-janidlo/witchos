using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using crass;

public class Window : MonoBehaviour, IPointerDownHandler
{
    public static Window FocusedWindow { get; private set; }

    public bool Focused => FocusedWindow == this;

    [Header("Data")]
    public string Title;
    public Sprite Icon;
    public ScriptableObject AppData;
    public bool Minimizable = true, Closable = true;
    public Minimizer Minimizer;

    public UnityEvent DidOpen, DidFocus;

    [Header("References")]
    public TextMeshProUGUI TitleText;
    public Image IconImage;
    public Button MinimizeButton, CloseButton;

    TaskBarButton taskBarButton;

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

        TimeState.Instance.DayEnded.AddListener(Close);

        DidOpen.Invoke();
    }

    void Update ()
    {
        MinimizeButton.interactable = Minimizable;
        CloseButton.interactable = Closable;

        TitleText.text = Title;
        IconImage.sprite = Icon;
    }

    public void SetTaskBarButton (TaskBarButton taskBarButton)
    {
        this.taskBarButton = taskBarButton;
        Minimizer.MinimizeTarget = taskBarButton.transform as RectTransform;
    }

    public void Close ()
    {
        TimeState.Instance.DayEnded.RemoveListener(Close);
        Destroy(gameObject);
        if (taskBarButton != null) Destroy(taskBarButton.gameObject);
    }

    public void Focus ()
    {
        transform.SetAsLastSibling(); // bring to front
        FocusedWindow = this;
        DidFocus.Invoke();
    }

	public void OnPointerDown (PointerEventData eventData)
	{
        Focus();
	}
}
