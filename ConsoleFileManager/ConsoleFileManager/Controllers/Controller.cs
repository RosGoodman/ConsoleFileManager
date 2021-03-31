using ConsoleFileManager.Controllers.Settings;
using ConsoleFileManager.Models;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Controls
{
    internal class Controller
    {
        private FileModel _selectedFile;    //выделенный файл
        private FileListModel _mainListFiles;   //список файлов 1 уровня
        private FileListModel _subListFiles;    //список файлов 2 уровня

        internal Settings SettingsControl { get; set; } //свойство для доступа к настройкам

        public Controller()
        {
            SettingsControl = new Settings(); //загружаем настройки
        }

        /// <summary>Получить информацию о файле.</summary>
        /// <returns>Информация построчно.</returns>
        public List<string> GetSelectedFileInfo()
        {
            List<string> fileInfo = _selectedFile.GetFileInfo();
            long size = 0;
            string createDate = string.Empty;

            if (!_selectedFile.IsFolder)    //если файл не является папкой
            {
                var file = new FileInfo(_selectedFile.FilePath);
                size = file.Length/1024;    //KB
                createDate = File.GetCreationTime(_selectedFile.FilePath).ToString();
            }

            fileInfo.Add(size.ToString() + "KB");
            fileInfo.Add(createDate);

            return fileInfo;
        }
    }
}
