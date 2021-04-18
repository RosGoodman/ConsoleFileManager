using System.Collections.Generic;

namespace ConsoleFileManager.Models
{
    public class FileListModel
    {
        private List<FileModel> _fileList = new List<FileModel>();  //список файлов в директории.

        public FileListModel(List<string> entries)
        {
            AddFilesInList(entries);
        }

        /// <summary>Создать экземпляры модели файлов и внести в список.</summary>
        /// <param name="entries">Массив файлов в директории.</param>
        internal void AddFilesInList(List<string> entries)
        {
            FileModel fileModel;
            foreach(string file in entries)
            {
                fileModel = new FileModel(file);
                _fileList.Add(fileModel);
            }
        }

        /// <summary>Удалить файл из списка.</summary>
        /// <param name="fullName">Полное имя файла (путь.)</param>
        internal void RemoveFileInList(string fullName)
        {
            foreach(FileModel file in _fileList)
            {
                if(file.FilePath == fullName)
                {
                    _fileList.Remove(file);
                    break;
                }
            }
        }

        /// <summary>Получить список файлов.</summary>
        /// <returns>Список файлов.</returns>
        public List<FileModel> GetFiles() => _fileList;
    }
}
