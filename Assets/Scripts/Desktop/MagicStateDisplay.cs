using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicStateDisplay : MonoBehaviour
{
    public string OffLabel, OnLabel, DepletedLabel;
    public TextMeshProUGUI StateDisplay;

    void Update ()
    {
        string label;

        var state = MagicSource.Instance.CurrentState;
        switch (state)
        {
            case MagicSource.State.Off:
                label = OffLabel;
                break;

            case MagicSource.State.On:
                label = OnLabel + remainingTimeString();
                break;

            case MagicSource.State.Depleted:
                label = DepletedLabel;
                break;

            default:
                throw new InvalidOperationException($"unexpected MagicSource.State value {state}");
        }

        StateDisplay.text = label;
    }

    string remainingTimeString ()
    {
        float seconds = MagicSource.Instance.RemainingOnTime;
        TimeSpan ts = TimeSpan.FromSeconds(seconds);
        return ts.ToString(@"mm\:ss");
    }
}
