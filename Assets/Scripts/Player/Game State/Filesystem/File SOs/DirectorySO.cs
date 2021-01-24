using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    [CreateAssetMenu(menuName = "WitchOS/File SO/Directory SO", fileName = "newDirectoryReference.asset", order = -1)]
    public class DirectorySO : FileSOBase
    {
        // since there are only two fields of metadata, and the chances of needing to add additional file metadata feel low, I'm just going to duplicate those two fields here. if I start adding any more file metadata fields, I should address this properly (ie, pulling the metadata out into a class)
        public string Filename;
        public Vector3 FileGuiPosition;

        public List<FileSOBase> FileSOs;

        [NonSerialized]
        Directory _directory = null;
        public override FileBase File => _directory ?? (_directory = createUnderlyingDirectory());

        Directory createUnderlyingDirectory ()
        {
            return new Directory(Filename)
            {
                Data = FileSOs.Select(fso => fso.File).ToList(),
                GuiPosition = FileGuiPosition
            };
        }
    }
}
