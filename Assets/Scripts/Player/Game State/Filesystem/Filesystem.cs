using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Filesystem", fileName = "NewFileSystem.asset")]
    public class FileSystem : ScriptableObject
    {
        public Directory RootDirectory => (initialized ? SaveData : initializeSaveData()).Value;

        public string PathSeparator;

        public SaveManager SaveManager;
        public DirectorySaveData SaveData;

        bool initialized;

        // GetFile methods will probably return null if nothing is found, rather than throw an exception
        public FileBase GetFileAtPath (string path, out Type fileDataType)
        {
            fileDataType = GetTypeOfFileAtPath(path);
            return GetFileAtPath(path, fileDataType);
        }

        public FileBase GetFileAtPath (string path, Type fileDataType)
        {
            Type actualTypeAtPath = GetTypeOfFileAtPath(path);

            if (actualTypeAtPath == null) return null; // should this actually throw? since this method kind of assumes that a file exists

            if (fileDataType != actualTypeAtPath) throw new Exception(); // TODO: determine type and message of this throw

            throw new NotImplementedException();
        }

        public File<T> GetFileAtPath<T> (string path)
        {
            return (File<T>) GetFileAtPath(path, typeof(T));
        }

        // will probably return null if file doesn't exist
        public Type GetTypeOfFileAtPath (string path)
        {
            throw new NotImplementedException();
        }

        public bool FileExistsAtPath (string path)
        {
            throw new NotImplementedException();
        }

        public string GetPathOfFile (FileBase file)
        {
            return recursiveFilePathSearch(file, RootDirectory, "");
        }

        public void AddFile (FileBase file, string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveFile (string path)
        {
            throw new NotImplementedException();
        }

        SaveData<Directory> initializeSaveData ()
        {
            SaveManager.Register(SaveData);
            initialized = true;

            return SaveData;
        }

        string recursiveFilePathSearch (FileBase target, FileBase current, string parentPath)
        {
            string currentPath = parentPath + current.Name;

            if (current == target)
            {
                return currentPath;
            }

            if (current is Directory)
            {
                foreach (var child in (current as Directory).Data)
                {
                    string childPath = recursiveFilePathSearch(target, child, currentPath + PathSeparator);

                    if (childPath != null) return childPath;
                }
            }

            return null;
        }
    }
}
