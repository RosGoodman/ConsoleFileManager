
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

        internal string FilePath => _filePath;  //путь к файлу.
        internal bool IsFolder => _isFolder;    //является ли файл папкой.

        //уровень глубины 0-корневая папка, 1-файлы/папки в корневой, 2-все в папках 1 уровня
        internal int DeepthLvl
        {
            get => _deepthLvl;
            set { _deepthLvl = value; }
        }

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
        /// <returns>Список информации (Имя файла, путь, является ли папкой).</returns>
        internal List<string> GetFileInfo()
        {
            List<string> fileInfo = new List<string>();
            fileInfo.Add(_fileName);
            fileInfo.Add(_filePath);
            fileInfo.Add(_isFolder.ToString());

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
