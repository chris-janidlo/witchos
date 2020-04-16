using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DesktopIcon : MonoBehaviour
{
    [Header("Data")]
    public Sprite Icon;
    public string Label;
    public Window ApplicationPrefab;
    public ScriptableObject ApplicationData;
    public bool OpenSingleInstance;
    public WindowEvent OnApplicationOpened;

    [Header("References")]
    public Image IconImage;
    public TextMeshProUGUI LabelText;
    public Button Button;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void Update ()
    {
        IconImage.sprite = Icon;
        LabelText.text = Label;
    }

    void onClick ()
    {
        var options = WindowFactory.Options.TaskBarButton;
        if (OpenSingleInstance) options |= WindowFactory.Options.Singleton;

        var application = WindowFactory.Instance.OpenWindow(ApplicationPrefab, ApplicationData, options);

        OnApplicationOpened.Invoke(application);
    }
}
