using System.Collections.Generic;

namespace ConsoleFileManager.Models
{
    internal class FileListModel
    {
        private List<FileModel> _fileList;  //список файлов в директории.

        public FileListModel(string[] entries)
        {
            AddFilesInList(entries);
        }


        /// <summary>Создать экземпляры модели файлов и внести в список.</summary>
        /// <param name="entries">Массив файлов в директории.</param>
        private void AddFilesInList(string[] entries)
        {
            FileModel fileModel;
            for (int i = 0; i < entries.Length; i++)
            {
                fileModel = new FileModel(entries[i]);
                _fileList.Add(fileModel);
            }
        }

        /// <summary>Получить список файлов.</summary>
        /// <returns>Список файлов.</returns>
        public List<FileModel> GetFiles() => _fileList;
    }
}
