using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using WitchOS.Editor;

namespace Tests
{
    public class PersistentDataPathToolTests
    {
        [Test]
        public void CorrectPathIsCopiedToBuffer ()
        {
            EditorGUIUtility.systemCopyBuffer = "";
            PersistentDataPathTool.CopyPersistentDataPath();
            Assert.AreEqual(Application.persistentDataPath, EditorGUIUtility.systemCopyBuffer);
        }
    }
}
