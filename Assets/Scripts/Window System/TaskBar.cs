using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using crass;

public class TaskBar : Singleton<TaskBar>
{
    public HorizontalLayoutGroup TaskBarButtonGroup;
    public TextMeshProUGUI Clock;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    void Update ()
    {
        Clock.text = TimeState.Instance.GetTimeString();
    }

    public void AddButton (TaskBarButton button)
    {
        button.transform.SetParent(TaskBarButtonGroup.transform, false);
    }
}
