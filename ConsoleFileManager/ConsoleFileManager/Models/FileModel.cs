
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

        internal string FilePath => _filePath;  //путь к файлу.
        internal bool IsFolder => _isFolder;    //является ли файл папкой.

        internal FileModel(string filePath)
        {
            _filePath = filePath;   //путь к файлу.

            if (Directory.Exists(filePath)) //папка\не папка.
                _isFolder = true;

            SetFileName();
        }

        /// <summary>Получить данные файла.</summary>
        /// <returns>Список информации.</returns>
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
