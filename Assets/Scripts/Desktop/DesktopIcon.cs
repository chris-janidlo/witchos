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
    public UnityEvent OnClick;

    [Header("References")]
    public Image IconImage;
    public TextMeshProUGUI LabelText;
    public Button Button;

    void Start ()
    {
        Button.onClick.AddListener(OnClick.Invoke);
    }

    void Update ()
    {
        IconImage.sprite = Icon;
        LabelText.text = Label;
    }
}
