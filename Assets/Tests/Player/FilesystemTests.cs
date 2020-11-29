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
        File<string> defaultFile = new File<string> { Name = "test", Data = "" };

        [SetUp]
        public void SetUp ()
        {
            fileSystem = ScriptableObject.CreateInstance<FileSystem>();

            fileSystem.PathSeparator = PATH_SEP;

            fileSystem.SaveData = new DirectorySaveData
            {
                FileName = "filesystem-tests",
                Value = new Directory { Data = new List<FileBase> { defaultFile } }
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
        public void PathIsCorrect_ForInitialFiles ()
        {
            var calculatedPath = fileSystem.RootDirectory.Name + PATH_SEP + fileSystem.RootDirectory.Data[0].Name;

            Assert.That(fileSystem.GetPathOfFile(fileSystem.RootDirectory), Is.EqualTo(fileSystem.RootDirectory.Name));
            Assert.That(fileSystem.GetPathOfFile(defaultFile), Is.EqualTo(calculatedPath));
        }
    }
}
