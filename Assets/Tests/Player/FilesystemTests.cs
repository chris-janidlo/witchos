using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Runtime.Serialization;

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
                Value = new Directory("") { Data = new List<FileBase> { defaultFile } }
            };

            fileSystem.SaveManager = ScriptableObject.CreateInstance<SaveManager>();

            fileSystem.Initialize();
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

            Assert.That(fileSystem.RootPath, Is.EqualTo(fileSystem.RootDirectory.Name));
            Assert.That(fileSystem.GetPathOfFile(defaultFile), Is.EqualTo(calculatedPath));
        }

        [TestCase("")]
        [TestCase("root")]
        [TestCase(PATH_SEP)]
        [TestCase(PATH_SEP + PATH_SEP)]
        [TestCase("root" + PATH_SEP + "root")]
        public void RootPath_IsAlwaysRootName (string rootName)
        {
            fileSystem.RootDirectory.Name = rootName;
            fileSystem.RebuildInternalStructures();
            Assert.That(fileSystem.RootPath, Is.EqualTo(rootName));
        }

        [Test]
        public void FilesWithSameName_ButDifferentLocation_ReturnDifferentPaths ()
        {
            var sameName = new File<string> { Name = defaultFile.Name, Data = "" };
            fileSystem.RootDirectory.Data.Add(new Directory("otherDirectory") { Data = new List<FileBase> { sameName } });
            fileSystem.RebuildInternalStructures();

            var pathOne = fileSystem.GetPathOfFile(defaultFile);
            var pathTwo = fileSystem.GetPathOfFile(sameName);

            Assert.That(pathOne, Is.Not.EqualTo(pathTwo));
        }

        [Test]
        public void CanAddFile ()
        {
            var fileToAdd = new File<int> { Name = $"totally not {defaultFile.Name}" };

            Assert.That(() => fileSystem.AddFile(fileToAdd, fileSystem.RootPath), Throws.Nothing);
        }

        static object[] InvalidFileCases =
        {
            new object[] { new TFile { },                               "a file with Name unassigned" },
            new object[] { new TFile { Name = "" },                     "a file with an empty name" },
            new object[] { new TFile { Name = $"path{PATH_SEP}sep" },   "a file with the path separator in its name" },
            new object[] { new TFile { Name = "test" },                 "two files with the same name" },
        };

        [TestCaseSource("InvalidFileCases")]
        public void CannotAddInvalidFile (FileBase file, string descriptionOfBadFile)
        {
            Assert.That
            (
                () => fileSystem.AddFile(file, fileSystem.RootPath),
                Throws.InvalidOperationException,
                $"attempting to add {descriptionOfBadFile} should fail"
            );
        }

        [Test]
        public void AddDeepFile_Fails_WhenDeepFlagNotSet ()
        {
            var fileToAdd = new File<char>();
            var path = fileSystem.RootPath + PATH_SEP + "dir" + PATH_SEP + "anotherDir";

            Assert.That(() => fileSystem.AddFile(fileToAdd, path), Throws.InvalidOperationException);
        }

        static readonly string[] paths = new string[] { "/", "/root", "/a/b/c", "/root/root/root" };
        static readonly string[] names = new string[] { "test", "root" };

        [Test]
        public void AddedPath_AlwaysEquals_RetrievedPath ([ValueSource("paths")] string parentPath, [ValueSource("names")] string fileName)
        {
            var fileToAdd = new File<float> { Name = fileName };
            fileSystem.AddFile(fileToAdd, parentPath, true);

            var calculatedPath = parentPath + PATH_SEP + fileName;
            var retrievedPath = fileSystem.GetPathOfFile(fileToAdd);

            Assert.That(retrievedPath, Is.EqualTo(calculatedPath));
        }

        [Test]
        public void CanRemoveFile ()
        {
            string defaultPath = fileSystem.GetPathOfFile(defaultFile);

            Assert.That(() => fileSystem.RemoveFile(defaultPath), Is.True);
        }

        [Test]
        public void CannotRemoveRoot ()
        {
            Assert.That(() => fileSystem.RemoveFile(fileSystem.RootDirectory), Throws.InvalidOperationException);
        }

        [Test]
        public void UnaddedFile_CantBeRemoved ()
        {
            Assert.That(() => fileSystem.RemoveFile(new TFile()), Throws.InvalidOperationException);
        }
    }

    [DataContract]
    public class TFile : File<string> { } // concrete file type with short name for typing
}
