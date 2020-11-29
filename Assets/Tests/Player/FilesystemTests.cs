using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace WitchOS.Tests
{
    public class FilesystemTests
    {
        const string PATH_SEP = "/";

        FileSystem fileSystem;

        [SetUp]
        public void SetUp ()
        {
            fileSystem = ScriptableObject.CreateInstance<FileSystem>();

            fileSystem.PathSeparator = PATH_SEP;

            var emptyTextFile = new File<string> { Data = "" };
            var root = new Directory { Data = new List<FileBase> { emptyTextFile } };

            fileSystem.SaveData = new DirectorySaveData
            {
                FileName = "filesystem-tests",
                Value = root
            };

            fileSystem.SaveManager = ScriptableObject.CreateInstance<SaveManager>();
        }

        [TearDown]
        public void TearDown ()
        {
            // should never call save methods during these tests. just in case that does happen though...
            fileSystem.SaveManager.DeleteAllSaveData();
        }

        [Test]
        public void SetUpTearDownTest () { }
    }
}
