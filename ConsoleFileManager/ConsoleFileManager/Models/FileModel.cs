
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Models
{
    /// <summary>Класс описывающий модель файлов и папок.</summary>
    public class FileModel
    {
        private string _fileName = string.Empty;
        private string _filePath = string.Empty;
        private bool _isFolder = false;
        private bool _folderIsOpen = false;
        private int _deepthLvl;

        /// <summary>путь к файлу.</summary>
        internal string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                SetFileName();
            }
        }

        /// <summary>является ли файл папкой.</summary>
        internal bool IsFolder => _isFolder;

        /// <summary>Уровень глубины 0-корневая папка, 1-файлы/папки в корневой, 2-все в папках 1 уровня</summary>
        internal int DeepthLvl
        {
            get => _deepthLvl;
            set { _deepthLvl = value; }
        }

        /// <summary>Открыта ли папка.</summary>
        internal bool FolderIsOpen
        {
            get => _folderIsOpen;
            set { _folderIsOpen = value; }
        }

        internal FileModel(string filePath)
        {
            _filePath = filePath;   //путь к файлу.

            if (Directory.Exists(filePath)) //папка\не папка.
                _isFolder = true;

            SetFileName();
        }

        /// <summary>Получить данные файла.</summary>
        /// <returns>Список информации (Имя файла, путь, размер, дата создания).</returns>
        internal List<string> GetFileInfo()
        {
            var file = new FileInfo(_filePath);

            List<string> fileInfo = new List<string>();
            fileInfo.Add(_fileName);
            fileInfo.Add(_filePath);

            if(!_isFolder)
            {
                fileInfo.Add((file.Length / 1024).ToString());
            }
            else fileInfo.Add("--");

            fileInfo.Add(File.GetCreationTime(_filePath).ToString());

            return fileInfo;
        }

        /// <summary>Задать имя файла/директории.</summary>
        private void SetFileName()
        {
            if (_isFolder)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_filePath);
                _fileName = directoryInfo.Name;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(_filePath);
                _fileName = fileInfo.Name;
            }
        }

        public override string ToString()
        {
            return _fileName;
        }
    }
}
