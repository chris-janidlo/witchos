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

        Filesystem filesystem;
        static File<string> defaultFile = new File<string> { Name = "test", Data = "" };

        [SetUp]
        public void SetUp ()
        {
            filesystem = ScriptableObject.CreateInstance<Filesystem>();

            filesystem.PathSeparator = PATH_SEP;

            filesystem.SaveData = new DirectorySaveData
            {
                FileName = "filesystem-tests",
                Value = new Directory("") { Data = new List<FileBase> { defaultFile } }
            };

            filesystem.SaveManager = ScriptableObject.CreateInstance<SaveManager>();

            filesystem.Initialize();
        }

        [TearDown]
        public void TearDown ()
        {
            // should never call save methods during these tests. just in case that does happen though...
            filesystem.SaveManager.DeleteAllSaveData();
        }

        [Test]
        public void PathIsCorrect_ForInitialFiles ()
        {
            var calculatedPath = filesystem.RootDirectory.Name + PATH_SEP + filesystem.RootDirectory.Data[0].Name;

            Assert.That(filesystem.RootPath, Is.EqualTo(filesystem.RootDirectory.Name + PATH_SEP));
            Assert.That(filesystem.GetPathOfFile(defaultFile), Is.EqualTo(calculatedPath));
        }

        [TestCase("")]
        [TestCase("root")]
        public void RootPath_IsAlwaysRootNamePlusPathSep (string rootName)
        {
            filesystem.RootDirectory.Name = rootName;
            Assert.That(filesystem.RootPath, Is.EqualTo(rootName + PATH_SEP));
        }

        [Test]
        public void FilesWithSameName_ButDifferentLocation_ReturnDifferentPaths ()
        {
            var sameName = new File<string> { Name = defaultFile.Name, Data = "" };
            filesystem.AddFile(sameName, "/otherDirectory", true);

            var pathOne = filesystem.GetPathOfFile(defaultFile);
            var pathTwo = filesystem.GetPathOfFile(sameName);

            Assert.That(pathOne, Is.Not.EqualTo(pathTwo));
        }

        [Test]
        public void CanAddFile ()
        {
            var fileToAdd = new File<int> { Name = $"totally not {defaultFile.Name}" };

            Assert.That(() => filesystem.AddFile(fileToAdd, filesystem.RootPath), Throws.Nothing);
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
                () => filesystem.AddFile(file, filesystem.RootPath),
                Throws.InstanceOf<FilesystemException>(),
                $"attempting to add {descriptionOfBadFile} should fail"
            );
        }

        [Test]
        public void AddDeepFile_Fails_WhenDeepFlagNotSet ()
        {
            var fileToAdd = new File<char>();
            var path = filesystem.RootPath + PATH_SEP + "dir" + PATH_SEP + "anotherDir";

            Assert.That(() => filesystem.AddFile(fileToAdd, path), Throws.InstanceOf<FilesystemException>());
        }

        static readonly string[] paths = new string[] { "", "/root", "/a/b/c", "/root/root/root" };
        static readonly string[] names = new string[] { "test", "root" };

        [Test]
        public void AddedPath_AlwaysEquals_RetrievedPath ([ValueSource("paths")] string parentPath, [ValueSource("names")] string fileName)
        {
            filesystem.RemoveFile(defaultFile);

            var fileToAdd = new File<float> { Name = fileName };
            filesystem.AddFile(fileToAdd, parentPath, true);

            var calculatedPath = parentPath + PATH_SEP + fileName;
            var retrievedPath = filesystem.GetPathOfFile(fileToAdd);

            Assert.That(retrievedPath, Is.EqualTo(calculatedPath));
        }

        [Test]
        public void CanRemoveFile ()
        {
            string defaultPath = filesystem.GetPathOfFile(defaultFile);

            Assert.That(() => filesystem.RemoveFile(defaultPath), Is.True);
        }

        [Test]
        public void CannotRemoveRoot ()
        {
            Assert.That(() => filesystem.RemoveFile(filesystem.RootDirectory), Throws.InstanceOf<AttemptedRootDirectoryDeletionException>());
        }

        [Test]
        public void UnaddedFile_CantBeRemoved ()
        {
            Assert.That(() => filesystem.RemoveFile(new TFile()), Throws.InstanceOf<FileDoesNotExistException>());
        }

        [TestCase("/foo")]
        [TestCase("/foo/")]
        [TestCase("/foo/test")]
        [TestCase("/foo/test/")]
        // same name as default file:
        [TestCase("/test/")]
        [TestCase("/test/test")]
        [TestCase("/test/test/")]
        public void GetFileAtPath_ReturnsNull_IfFileDoesNotExist (string path)
        {
            Assert.That(filesystem.GetFileAtPath(path), Is.Null);
        }

        [TestCase("foo")]
        [TestCase("test")] // same name -> should do nothing
        public void CanRenameFile (string newName)
        {
            Assert.That(() => filesystem.RenameFile(defaultFile, newName), Throws.Nothing);
        }

        [Test]
        public void CannotRenameFile_IfFileWithSamePathAlreadyExists ()
        {
            filesystem.AddFile(new TFile { Name = "foo" }, "/bar/", true);
            filesystem.MoveFile(defaultFile, "/bar/");

            Assert.That(() => filesystem.RenameFile(defaultFile, "foo"), Throws.InstanceOf<PathAlreadyExistsException>());
        }

        [Test]
        public void DeepAddFails_WhenDirectoryNameIsEqualToExistingFilename ()
        {
            Assert.That(() => filesystem.AddFile(new TFile { Name = "blah" }, $"/{defaultFile.Name}/", true), Throws.InstanceOf<PathAlreadyExistsException>());
        }

        [Test]
        public void CanGetDirectoryPath_WithAndWithoutPathSeparator ()
        {
            var dir = new Directory("dir");
            filesystem.AddFile(dir, filesystem.RootDirectory);

            Assert.That(filesystem.GetFileAtPath("/dir"), Is.EqualTo(dir));
            Assert.That(filesystem.GetFileAtPath("/dir/"), Is.EqualTo(dir));
        }

        [Test]
        public void CanGetRootPath_WithAndWithoutPathSeparator ()
        {
            Assert.That(filesystem.GetFileAtPath(""), Is.EqualTo(filesystem.RootDirectory));
            Assert.That(filesystem.GetFileAtPath("/"), Is.EqualTo(filesystem.RootDirectory));
        }

        [Test]
        public void DeepAdd_DoesNotSkipExistingDirectories ()
        {
            filesystem.AddFile(new Directory("subOne"), filesystem.RootDirectory);
            var file = new TFile { Name = "baz" };

            filesystem.AddFile(file, "/subOne/subTwo", true);
            Assert.That(filesystem.GetPathOfFile(file), Is.EqualTo("/subOne/subTwo/baz"));
        }

        [Test]
        public void AddingDirectory_AddsContents ()
        {
            var files = new List<TFile>
            {
                new TFile { Name = "file0" },
                new TFile { Name = "file1" },
                new TFile { Name = "file2" },
                new TFile { Name = "file3" }
            };

            var dirSubSub = new Directory("subSubDirectory") { Data = new List<FileBase> { files[3] } };
            var dirSub = new Directory("subDirectory") { Data = new List<FileBase> { files[2], dirSubSub } };
            var dirMain = new Directory("fullDirectory") { Data = new List<FileBase> { files[1], files[0], dirSub } };

            filesystem.AddFile(dirMain, filesystem.RootDirectory);

            foreach (var file in files)
            {
                Assert.That(filesystem.FileExistsInFilesystem(file), $"file {file.Name} should exist");
            }
        }

        [Test]
        public void MovingDirectory_MovesContents ()
        {
            var fileA = new TFile() { Name = "a" };
            var fileB = new TFile() { Name = "b" };

            filesystem.AddFile(fileA, "/originalDir", true);
            filesystem.AddFile(fileB, "/originalDir/subDir", true);

            filesystem.AddFile(new Directory("newDirectory"), filesystem.RootDirectory);

            filesystem.MoveFile("/originalDir", "/newDirectory");

            Assert.That(filesystem.FileExistsAtPath("/newDirectory/originalDir"));

            Assert.That(filesystem.GetPathOfFile(fileA), Is.EqualTo("/newDirectory/originalDir/a"));
            Assert.That(filesystem.GetPathOfFile(fileB), Is.EqualTo("/newDirectory/originalDir/subDir/b"));
        }

        [Test]
        public void RemovingDirectory_RemovesContents ()
        {
            var fileA = new TFile() { Name = "a" };
            var fileB = new TFile() { Name = "b" };

            filesystem.AddFile(fileA, "/directoryToDelete", true);
            filesystem.AddFile(fileB, "/directoryToDelete/subDir", true);

            Assert.That(filesystem.FileExistsInFilesystem(fileA), "file A should exist in the filesystem at this point");
            Assert.That(filesystem.FileExistsInFilesystem(fileB), "file B should exist in the filesystem at this point");

            filesystem.RemoveFile("/directoryToDelete");

            Assert.That(filesystem.FileExistsInFilesystem(fileA), Is.False, "file A should no longer exist in the filesystem");
            Assert.That(filesystem.FileExistsInFilesystem(fileB), Is.False, "file B should no longer exist in the filesystem");
        }
    }

    [DataContract]
    public class TFile : File<string> { } // concrete file type with short name for typing
}
