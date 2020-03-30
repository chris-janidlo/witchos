using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppIcon : MonoBehaviour
{
    public Window WindowPrefab;
    public bool OnlyOpenOnce;

    public void LaunchApplication ()
    {
        WindowFactory.Instance.CreateWindowWithTaskbarButton(WindowPrefab, OnlyOpenOnce);
    }
}
