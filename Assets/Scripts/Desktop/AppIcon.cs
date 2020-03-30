using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppIcon : MonoBehaviour
{
    public Window WindowPrefab;
    public bool OnlyOpenOnce;

    public void LaunchApplication ()
    {
        if (OnlyOpenOnce)
        {
            WindowFactory.Instance.CreateSingletonWindowWithTaskbarButton(WindowPrefab);
        }
        else
        {
            WindowFactory.Instance.CreateWindowWithTaskbarButton(WindowPrefab);
        }
    }
}
