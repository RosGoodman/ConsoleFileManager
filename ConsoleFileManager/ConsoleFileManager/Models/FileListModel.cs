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
        private void AddFilesInList(List<string> entries)
        {
            FileModel fileModel;
            foreach(string file in entries)
            {
                fileModel = new FileModel(file);
                _fileList.Add(fileModel);
            }
        }

        /// <summary>Получить список файлов.</summary>
        /// <returns>Список файлов.</returns>
        public List<FileModel> GetFiles() => _fileList;
    }
}
