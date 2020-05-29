using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TaskBarButton : MonoBehaviour
{
    public UnityEvent Destroyed;

    public TextMeshProUGUI TitleText;
    public Image IconImage;
    public Button Button;

    Window window;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void Update ()
    {
        TitleText.text = window.Title;
        IconImage.sprite = window.Icon;
    }

    void OnDestroy ()
    {
        Destroyed.Invoke();
    }

    public void SetWindow (Window window)
    {
        this.window = window;
    }

    void onClick ()
    {
        if (window.Minimizer.Minimized)
        {
            window.Minimizer.UnMinimize();
            window.Focus();
        }
        else if (!window.Focused)
        {
            window.Focus();
        }
        else
        {
            window.Minimizer.Minimize();
        }
    }
}
