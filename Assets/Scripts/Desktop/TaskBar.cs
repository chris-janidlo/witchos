using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using crass;

public class TaskBar : Singleton<TaskBar>
{
    public HorizontalLayoutGroup TaskBarButtonGroup;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public void AddButton (TaskBarButton button)
    {
        button.transform.SetParent(TaskBarButtonGroup.transform, false);
    }
}
