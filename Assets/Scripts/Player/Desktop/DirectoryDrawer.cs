using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DirectoryDrawer : MonoBehaviour
    {
        public RectTransform DesktopIconParent;
        public DesktopIcon DesktopIconPrefab;

        public FileAssociationConfig FileAssociationConfig;

        public void Draw (Directory directory)
        {
            foreach (var file in directory.Data)
            {
                var iconPrefab = FileAssociationConfig.GetMetadataForFile(file).DesktopIconPrefabOverride ?? DesktopIconPrefab;
                var icon = Instantiate(iconPrefab, file.GuiPosition, Quaternion.identity, DesktopIconParent);
                icon.File = file;
            }
        }
    }
}
