using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSwitcher : MonoBehaviour
{
    public int CurrentTabState;
    public List<Button> TabButtons;
    public List<GameObject> TabViews;

    void Start ()
    {
        if (TabButtons.Count != TabViews.Count)
        {
            throw new InvalidOperationException("TabButtons and TabViews must be the same length");
        }

        Action<int> setTabState = newStateNum =>
        {
            for (int i = 0; i < TabViews.Count; i++)
            {
                TabButtons[i].interactable = newStateNum != i;
                TabViews[i].SetActive(newStateNum == i);
            }

            CurrentTabState = newStateNum;
        };

        for (int j = 0; j < TabViews.Count; j++)
        {
            int copy = j;
            TabButtons[j].onClick.AddListener(() => setTabState(copy));
        }

        setTabState(CurrentTabState);
    }
}
