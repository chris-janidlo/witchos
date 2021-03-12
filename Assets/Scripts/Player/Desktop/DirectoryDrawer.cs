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
            Filesystem.MoveFile(icon.File, directory); // this should always happen first, so that any exceptions can happen before we change anything else
            icon.transform.SetParent(DesktopIconParent, true);
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
