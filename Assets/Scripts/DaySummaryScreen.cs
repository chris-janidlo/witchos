using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using crass;

public class DaySummaryScreen : Singleton<DaySummaryScreen>
{
    public RectTransform Container;
    public TextMeshProUGUI TasksCompleted, TotalTasks, SuccessRate;
    public Button DoneButton;

    void Awake ()
    {
        SingletonOverwriteInstance(this);

        DoneButton.onClick.AddListener(closeSummary);
        Container.gameObject.SetActive(false);
    }

    public void ShowSummary ()
    {
        int complete = MailState.Instance.TasksCompleted, total = MailState.Instance.TotalTasks;

        TasksCompleted.text = complete.ToString();
        TotalTasks.text = total.ToString();
        SuccessRate.text = ((float) complete / total).ToString("P2");

        Container.gameObject.SetActive(true);
    }

    void closeSummary ()
    {
        Container.gameObject.SetActive(false);
        TimeState.Instance.StartNewDay();
    }
}
