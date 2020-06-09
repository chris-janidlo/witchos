using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using crass;

namespace WitchOS
{
public class TaskBar : Singleton<TaskBar>
{
    public int ButtonCount { get; private set; }

    public int OverflowThreshold;

    public HorizontalLayoutGroup TaskBarButtonGroup;
    public ContentSizeFitter TaskBarSizeFitter;

    Vector2 originalSizeDelta;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    void Start ()
    {
        originalSizeDelta = (TaskBarSizeFitter.transform as RectTransform).sizeDelta;
    }

    public void AddButton (TaskBarButton button)
    {
        button.transform.SetParent(TaskBarButtonGroup.transform, false);
        button.Destroyed.AddListener(() => {
            ButtonCount--;
            setOverflowState();
        });

        ButtonCount++;
        setOverflowState();
    }

    void setOverflowState ()
    {
        bool overflowing = ButtonCount > OverflowThreshold;

        TaskBarSizeFitter.horizontalFit = overflowing
            ? ContentSizeFitter.FitMode.MinSize
            : ContentSizeFitter.FitMode.Unconstrained;

        if (!overflowing)
            (TaskBarSizeFitter.transform as RectTransform).sizeDelta = originalSizeDelta;
    }
}
}
