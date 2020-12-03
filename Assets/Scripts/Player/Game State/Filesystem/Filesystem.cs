using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Filesystem", fileName = "NewFileSystem.asset")]
    public class FileSystem : ScriptableObject
    {
        public Directory RootDirectory => SaveData.Value;
        public string RootPath => GetPathOfFile(RootDirectory);

        public string PathSeparator;

        public SaveManager SaveManager;
        public DirectorySaveData SaveData;

        Dictionary<FileBase, Directory> parentCache;
        List<FilePathAssociation> filePathAssociations;

        bool initialized;

        class FilePathAssociation
        {
            public FileBase File;
            public List<string> ValidPaths;

            public FilePathAssociation (FileBase file, params string[] paths)
            {
                File = file;
                ValidPaths = paths.ToList();
            }
        }

        // initializes the filesystem, if necessary
        public void Initialize ()
        {
            if (initialized) return;

            SaveManager.Register(SaveData);
            buildDataStructures();

            initialized = true;
        }

        public FileBase GetFileAtPath (string path)
        {
            return filePathAssociations.FirstOrDefault(fpa => fpa.ValidPaths.Contains(path))?.File;
        }

        public FileBase GetFileAtPath (string path, out Type fileDataType)
        {
            fileDataType = GetTypeOfFileDataAtPath(path);
            return GetFileAtPath(path);
        }

        public FileBase GetFileAtPath (string path, Type fileDataType)
        {
            Type actualTypeAtPath = GetTypeOfFileDataAtPath(path);

            if (actualTypeAtPath == null || fileDataType != actualTypeAtPath)
            {
                return null;
            }

            return GetFileAtPath(path);
        }

        public File<T> GetFileAtPath<T> (string path)
        {
            return (File<T>) GetFileAtPath(path, typeof(T));
        }

        public Directory GetDirectoryAtPath (string path)
        {
            return GetFileAtPath<List<FileBase>>(path) as Directory;
        }

        // returns null if file doesn't exist
        public Type GetTypeOfFileDataAtPath (string path)
        {
            var file = GetFileAtPath(path);

            if (file == null)
            {
                return null;
            }

            var type = file.GetType().GetField("Data").FieldType;

            return type;
        }

        public bool FileExistsInFileSystem (FileBase file)
        {
            return parentCache.ContainsKey(file);
        }

        public bool FileExistsAtPath (string path)
        {
            return filePathAssociations.Any(fpa => fpa.ValidPaths.Contains(path));
        }

        public string GetPathOfFile (FileBase file)
        {
            if (!FileExistsInFileSystem(file))
            {
                throw new InvalidOperationException($"file {file.Name} does not exist in this filesystem");
            }

            return filePathAssociations.First(fpa => fpa.File == file).ValidPaths.First();
        }

        public void RenameFile (FileBase file, string name)
        {
            if (!FileExistsInFileSystem(file))
            {
                throw new InvalidOperationException("cannot rename file that does not exist");
            }

            if (file.Name == name) return;

            if (file != RootDirectory && parentCache[file].Data.Any(f => f != file && f.Name == name))
            {
                throw new InvalidOperationException($"cannot rename {file.Name} to {name} because its parent directory ({parentCache[file].Name}{PathSeparator}) already contains a file with that name");
            }

            file.Name = name;
            buildDataStructures();
        }

        public void RenameFile (string filePath, string name)
        {
            RenameFile(GetFileAtPath(filePath), name);
        }

        // deep parameter acts like the 'p' flag in mkdir. if it's set and you want to add path '/a/b/c' and directory 'a' or 'b' do not exist, they will be created
        public void AddFile (FileBase file, string parentDirectoryPath, bool deep = false)
        {
            if (FileExistsInFileSystem(file))
            {
                throw new InvalidOperationException($"cannot add file {file.Name} because it already exists in this filesystem");
            }

            if (string.IsNullOrEmpty(file.Name))
            {
                throw new InvalidOperationException("files must be named in order to be added");
            }

            if (file.Name.Contains(PathSeparator))
            {
                throw new InvalidOperationException($"file {file.Name} cannot be added because it contains the path separator ({PathSeparator}) in its name");
            }

            string addedPath = parentDirectoryPath + PathSeparator + file.Name;

            if (FileExistsAtPath(addedPath))
            {
                throw new InvalidOperationException($"cannot add file with path {addedPath} because one already exists");
            }

            Directory parent = GetDirectoryAtPath(parentDirectoryPath);

            if (parent == null)
            {
                if (!deep) throw new InvalidOperationException($"one or more directories on the path {parentDirectoryPath} do not exist. either add those directories, or set the deep flag and try again");

                // gah why can't we just call the thing directly
                string[] directoryNames = parentDirectoryPath.Split(new string[] { PathSeparator }, StringSplitOptions.None);

                string[] fullDirectoryPaths = new string[directoryNames.Length];
                fullDirectoryPaths[0] = "";

                parent = RootDirectory;

                for (int i = 1; i < directoryNames.Length; i++)
                {
                    fullDirectoryPaths[i] = fullDirectoryPaths[i - 1] + PathSeparator + directoryNames[i];

                    if (FileExistsAtPath(fullDirectoryPaths[i])) continue;

                    var newDirectory = new Directory(directoryNames[i]);
                    addFileToInternalStructures(newDirectory, parent, fullDirectoryPaths[i]);

                    parent = newDirectory;
                }
            }

            addFileToInternalStructures(file, parent, addedPath);
        }

        public void AddFile (FileBase file, Directory parent)
        {
            if (FileExistsInFileSystem(file))
            {
                throw new InvalidOperationException($"cannot add file {file.Name} because it already exists in this filesystem");
            }

            string addedPath = GetPathOfFile(parent) + PathSeparator + file.Name;

            if (FileExistsAtPath(addedPath))
            {
                throw new InvalidOperationException($"cannot add file with path {addedPath} because one already exists");
            }

            addFileToInternalStructures(file, parent, addedPath);
        }

        public void MoveFile (string fromPath, string toPath, bool deep = false)
        {
            var file = GetFileAtPath(fromPath);
            MoveFile(file, toPath, deep);
        }

        public void MoveFile (FileBase file, string toPath, bool deep = false)
        {
            RemoveFile(file);
            AddFile(file, toPath, deep);
        }

        public void MoveFile (string fromPath, Directory newParent)
        {
            var file = GetFileAtPath(fromPath);
            MoveFile(file, newParent);
        }

        public void MoveFile (FileBase file, Directory newParent)
        {
            RemoveFile(file);
            AddFile(file, newParent);
        }

        // returns whether file existed
        public bool RemoveFile (string path)
        {
            var file = GetFileAtPath(path);

            if (file == null)
            {
                return false;
            }

            RemoveFile(file);

            return true;
        }

        // assumes file existed to begin with
        public void RemoveFile (FileBase file)
        {
            if (!FileExistsInFileSystem(file))
            {
                throw new InvalidOperationException($"file {file.Name} does not exist in this filesystem");
            }

            if (file == RootDirectory)
            {
                throw new InvalidOperationException("cannot delete the root directory");
            }

            parentCache[file].Data.Remove(file);
            parentCache.Remove(file);

            filePathAssociations.RemoveAll(fpa => fpa.File == file);
        }

        void buildDataStructures ()
        {
            parentCache = new Dictionary<FileBase, Directory>();
            filePathAssociations = new List<FilePathAssociation>();

            buildDataStructuresRecursive(SaveData.Value, null, "");
        }

        void buildDataStructuresRecursive (FileBase file, Directory parent, string parentPath)
        {
            var currentPath = parentPath + file.Name;

            parentCache[file] = parent;
            addFPA(file, currentPath);

            if (file is Directory dir)
            {
                foreach (var child in dir.Data)
                {
                    buildDataStructuresRecursive(child, dir, currentPath + PathSeparator);
                }
            }
        }

        void addFileToInternalStructures (FileBase file, Directory parent, string filePath)
        {
            parent.Data.Add(file);

            parentCache[file] = parent;
            addFPA(file, filePath);
        }

        void addFPA (FileBase file, string path)
        {
            var fpa = file is Directory
                ? new FilePathAssociation(file, path, path + PathSeparator)
                : new FilePathAssociation(file, path);

            filePathAssociations.Add(fpa);
        }
    }
}
