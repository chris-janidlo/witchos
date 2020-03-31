using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using crass;

public class Window : MonoBehaviour, IPointerDownHandler
{
    public static Window FocusedWindow { get; private set; }

    public event Action DidFocus;

    public bool Minimized { get; private set; }
    public bool Focused => FocusedWindow == this;

    [Header("Data")]
    public string Title;
    public Sprite Icon;
    public bool Minimizable = true, Closable = true;
    public TransitionableFloat MinimizeTransition;

    [Header("References")]
    public TextMeshProUGUI TitleText;
    public Image IconImage;
    public Button MinimizeButton, CloseButton;

    TaskBarButton taskBarButton;
    Vector2 trueLocation;

    void Start ()
    {
        // check if buttons are enabled here and not in methods so that code can minimize the window regardless of user-interactability state

        MinimizeButton.onClick.AddListener
        (
            () => { if (Minimizable) Minimize(); }
        );

        CloseButton.onClick.AddListener
        (
            () => { if (Closable) Close(); }
        );

        MinimizeTransition.AttachMonoBehaviour(this);
    }

    void Update ()
    {
        MinimizeButton.interactable = Minimizable;
        CloseButton.interactable = Closable;

        TitleText.text = Title;
        IconImage.sprite = Icon;

        if (MinimizeTransition.Value == 1)
        {
            trueLocation = transform.position;
        }

        doMinimizeAnimation();
    }

    public void SetTaskBarButton (TaskBarButton taskBarButton)
    {
        this.taskBarButton = taskBarButton;
    }

    public void Minimize ()
    {
        if (Minimized) return;

        MinimizeTransition.StartTransitionTo(0);

        Minimized = true;
    }

    public void UnMinimize ()
    {
        if (!Minimized) return;

        MinimizeTransition.StartTransitionTo(1);

        Minimized = false;
    }

    public void Close ()
    {
        Destroy(gameObject);
        Destroy(taskBarButton.gameObject);
    }

    public void Focus ()
    {
        transform.SetAsLastSibling(); // bring to front
        FocusedWindow = this;
        DidFocus?.Invoke();
    }

	public void OnPointerDown (PointerEventData eventData)
	{
        Focus();
	}

    void doMinimizeAnimation ()
    {
        float scalar = MinimizeTransition.Value;

        transform.localScale = Vector2.one * scalar;
        transform.position = Vector2.Lerp
        (
            taskBarButton.transform.position,
            trueLocation,
            scalar
        );
    }
}
