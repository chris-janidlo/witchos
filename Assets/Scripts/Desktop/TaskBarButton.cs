﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskBarButton : MonoBehaviour
{
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

    public void SetWindow (Window window)
    {
        this.window = window;
    }

    void onClick ()
    {
        if (window.Minimized)
        {
            window.UnMinimize();
            window.Focus();
        }
        else if (!window.Focused)
        {
            window.Focus();
        }
        else
        {
            window.Minimize();
        }
    }
}