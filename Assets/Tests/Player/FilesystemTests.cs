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
        static File<string> defaultFile = new File<string> { Name = "test", Data = "" };

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

            Assert.That(fileSystem.RootPath, Is.EqualTo(fileSystem.RootDirectory.Name + PATH_SEP));
            Assert.That(fileSystem.GetPathOfFile(defaultFile), Is.EqualTo(calculatedPath));
        }

        [TestCase("")]
        [TestCase("root")]
        [TestCase(PATH_SEP)]
        [TestCase(PATH_SEP + PATH_SEP)]
        [TestCase("root" + PATH_SEP + "root")]
        public void RootPath_IsAlwaysRootNamePlusPathSep (string rootName)
        {
            fileSystem.RenameFile(fileSystem.RootDirectory, rootName);
            Assert.That(fileSystem.RootPath, Is.EqualTo(rootName + PATH_SEP));
        }

        [Test]
        public void FilesWithSameName_ButDifferentLocation_ReturnDifferentPaths ()
        {
            var sameName = new File<string> { Name = defaultFile.Name, Data = "" };
            fileSystem.AddFile(sameName, "/otherDirectory", true);

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
            new object[] { defaultFile,                                 "a file that already exists" },
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

        static readonly string[] paths = new string[] { "", "/root", "/a/b/c", "/root/root/root" };
        static readonly string[] names = new string[] { "test", "root" };

        [Test]
        public void AddedPath_AlwaysEquals_RetrievedPath ([ValueSource("paths")] string parentPath, [ValueSource("names")] string fileName)
        {
            fileSystem.RemoveFile(defaultFile);

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

        [TestCase("/abc")]
        [TestCase("/test/")]
        [TestCase("/test/test")]
        [TestCase("/test/test/")]
        public void GetFileAtPath_ReturnsNull_IfFileDoesNotExist (string path)
        {
            Assert.That(fileSystem.GetFileAtPath(path), Is.Null, "base type method should return null");
            Assert.That(fileSystem.GetFileAtPath(path, out _), Is.Null, "out type method should return null");
            Assert.That(fileSystem.GetFileAtPath(path, typeof(object)), Is.Null, "passed-in type method should return null");
            Assert.That(fileSystem.GetFileAtPath<object>(path), Is.Null, "generic method should return null");
        }

        [TestCase("foo")]
        [TestCase("test")] // same name -> should do nothing
        public void CanRenameFile (string newName)
        {
            Assert.That(() => fileSystem.RenameFile(defaultFile, newName), Throws.Nothing);
        }

        [Test]
        public void CannotRenameFile_IfFileWithSamePathAlreadyExists ()
        {
            fileSystem.AddFile(new TFile { Name = "foo" }, "/bar/", true);
            fileSystem.MoveFile(defaultFile, "/bar/");

            Assert.That(() => fileSystem.RenameFile(defaultFile, "foo"), Throws.InvalidOperationException);
        }

        [Test]
        public void DeepAddFails_WhenDirectoryNameIsEqualToExistingFilename ()
        {
            Assert.That(() => fileSystem.AddFile(new TFile { Name = "blah" }, $"/{defaultFile.Name}/", true), Throws.InvalidOperationException);
        }

        [Test]
        public void CanGetDirectoryPath_WithAndWithoutPathSeparator ()
        {
            var dir = new Directory("dir");
            fileSystem.AddFile(dir, fileSystem.RootDirectory);

            Assert.That(fileSystem.GetFileAtPath("/dir"), Is.EqualTo(dir));
            Assert.That(fileSystem.GetFileAtPath("/dir/"), Is.EqualTo(dir));
        }

        [Test]
        public void CanGetRootPath_WithAndWithoutPathSeparator ()
        {
            Assert.That(fileSystem.GetFileAtPath(""), Is.EqualTo(fileSystem.RootDirectory));
            Assert.That(fileSystem.GetFileAtPath("/"), Is.EqualTo(fileSystem.RootDirectory));
        }

        [Test]
        public void DeepAdd_DoesNotSkipExistingDirectories ()
        {
            fileSystem.AddFile(new Directory("subOne"), fileSystem.RootDirectory);
            var file = new TFile { Name = "baz" };

            fileSystem.AddFile(file, "/subOne/subTwo", true);
            Assert.That(fileSystem.GetPathOfFile(file), Is.EqualTo("/subOne/subTwo/baz"));
        }
    }

    [DataContract]
    public class TFile : File<string> { } // concrete file type with short name for typing
}
