using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DirectoryDrawer : MonoBehaviour
    {
        public RectTransform DesktopIconParent;
        public DesktopIcon DesktopIconPrefab;

        public Filesystem Filesystem;
        public FileAssociationConfig FileAssociationConfig;

        Directory directory;

        public void Initialize (Directory directory)
        {
            this.directory = directory;
            spawnIcons();
        }

        public void AddIcon (DesktopIcon icon)
        {
            icon.transform.SetParent(DesktopIconParent, true);
            Filesystem.MoveFile(icon.File, directory);
        }

        void spawnIcons ()
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
