using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace WitchOS.Editor.Tests
{
    public class PersistentDataPathToolTests
    {
        [Test]
        public void CorrectPathIsCopiedToBuffer ()
        {
            string oldCopyBuffer = EditorGUIUtility.systemCopyBuffer;
            EditorGUIUtility.systemCopyBuffer = "";

            PersistentDataPathTool.CopyPersistentDataPath();
            Assert.AreEqual(Application.persistentDataPath, EditorGUIUtility.systemCopyBuffer);

            EditorGUIUtility.systemCopyBuffer = oldCopyBuffer;
        }
    }
}
