
using System.Collections.Generic;

namespace ConsoleFileManager.Models
{
    abstract class FileList
    {
        private List<FileModel> _fileList;

        public FileList(string[] entries)
        {
            //TODO: вынести в controller
            //список всех файлов указанной директории.
            //string[] entries = Directory.GetFileSystemEntries(workDir, "*", SearchOption.AllDirectories);
        }

        /// <summary>Получить список файлов.</summary>
        /// <returns>Список файлов.</returns>
        public List<FileModel> GetFiles() => _fileList;
    }
}
