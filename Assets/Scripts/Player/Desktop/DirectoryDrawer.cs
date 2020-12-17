using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DirectoryDrawer : MonoBehaviour
    {
        public RectTransform DesktopIconParent;
        public DesktopIcon DesktopIconPrefab;

        public void Draw (Directory directory)
        {
            foreach (var file in directory.Data)
            {
                var icon = Instantiate(DesktopIconPrefab, file.GuiPosition, Quaternion.identity, DesktopIconParent);
                icon.File = file;
            }
        }
    }
}
