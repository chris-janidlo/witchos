using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using crass;

public class DaySummaryScreen : Singleton<DaySummaryScreen>
{
    public RectTransform Container;
    public TextMeshProUGUI TasksCompleted, Revenue, Expenses, Total;
    public Button DoneButton;

    void Awake ()
    {
        SingletonOverwriteInstance(this);

        DoneButton.onClick.AddListener(closeSummary);
        Container.gameObject.SetActive(false);
    }

    public void ShowSummary ()
    {
        // TODO:
        // TasksCompleted.text = 
        // Revenue.text = 
        // Total.text = 

        Container.gameObject.SetActive(true);
    }

    void closeSummary ()
    {
        Container.gameObject.SetActive(false);
        TimeState.Instance.StartNewDay();
    }
}
