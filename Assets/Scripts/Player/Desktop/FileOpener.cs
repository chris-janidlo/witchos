using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class FileOpener : MonoBehaviour
    {
        public FileSOBase FileSO;

        public void Open ()
        {
            WindowFactory.Instance.OpenWindowWithFile(FileSO.File);
        }
    }
}
