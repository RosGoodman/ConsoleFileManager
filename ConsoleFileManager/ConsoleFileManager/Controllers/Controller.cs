using ConsoleFileManager.Controllers.Services;
using ConsoleFileManager.Controllers.Settings;
using ConsoleFileManager.Models;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Controls
{
    public class Controller
    {
        private FileModel _selectedFile;    //выделенный файл
        private FileListModel _mainListFiles;   //список файлов 1 уровня
        private FileListModel _subListFiles;    //список файлов 2 уровня

        private Settings SettingsControl { get; set; } //свойство для доступа к настройкам

        public Controller()
        {
            SettingsControl = new Settings(); //загружаем настройки
        }

        #region SelectedFileInfo

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

        #endregion

        #region SettingsControl interface

        /// <summary>Установить новое значение параметра.</summary>
        /// <param name="settingName">Наименование параметра.</param>
        /// <param name="value">Новое значение параметра.</param>
        public void SetNewSettingValue(string settingName, string value)
            => SettingsControl.ChangeProperty(settingName, value);

        /// <summary>Загрузить сохраненные настройки.</summary>
        public void LoadSettings() => SettingsControl.LoadSettings();

        /// <summary>Сохранить настройки.</summary>
        public void SaveSettings() => SettingsControl.SaveSettings();

        #endregion

        #region commands

        /// <summary>Удалить файл.</summary>
        /// <param name="filename">Имя удаляемого файла.</param>
        internal void DeletingFile(string filename)
        {
            WorkWithFiles.DeletingFile(filename);
        }

        /// <summary>Создать папку.</summary>
        /// <param name="newDirName">Имя создаваемой папки.</param>
        internal void CreateDirectory(string newDirName)
        {
            string isFolder = _selectedFile.GetFileInfo()[2];
            if (isFolder == "True")
            {
                WorkWithFiles.CreatingDirectory(newDirName, GetRootFolder());
            }
            else
            {
                //сообщение об ошибке - выбран файл, а не папка
            }
        }

        /// <summary>Переименовать выбранный файл/папку.</summary>
        /// <param name="newName">Новое имя файла.</param>
        internal void Rename(string newName)
        {
            string oldName = _selectedFile.GetFileInfo()[1];
            WorkWithFiles.Renaming(newName, oldName);
        }

        /// <summary>Получить корневую папку текущего выбранного файла.</summary>
        /// <returns>Путь к корневой папке.</returns>
        private string GetRootFolder()
        {
            string selectedFilePath = _selectedFile.GetFileInfo()[1];
            string rootPath = selectedFilePath.Substring(0, selectedFilePath.LastIndexOf('\\'));
            return rootPath;
        }

        #endregion
    }
}
