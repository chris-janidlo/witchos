using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DesktopIconDragContainer : MonoBehaviour
    {
        public void OnDesktopIconBeganDragging (GameObject icon)
        {
            icon.transform.SetParent(transform, true);
        }
    }
}
