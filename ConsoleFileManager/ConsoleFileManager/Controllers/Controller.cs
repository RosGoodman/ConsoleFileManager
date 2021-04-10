﻿using ConsoleFileManager.Controllers.Services;
using ConsoleFileManager.Controllers.Settings;
using ConsoleFileManager.Models;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Controls
{
    public class Controller
    {
        private FileModel _selectedFile;        //выделенный файл
        private FileListModel _mainListFiles;   //список файлов 1 уровня
        private FileListModel _subListFiles;    //список файлов 2 уровня

        public delegate void ChangeSelectedFileHandler(FileModel selectedFile);
        public event ChangeSelectedFileHandler Notify;          //определение события изменения выделенного элемента

        public delegate void ChangeMainListFiles(FileListModel mainMainListFiles);
        public event ChangeMainListFiles changeMainListNotify;  //определение события измененния списка файлов 1 уровня

        public delegate void ChangeSubListFiles(FileListModel subListFiles);
        public event ChangeMainListFiles changeSubListNotify;   //определение события измененния списка файлов 2 уровня

        #region Properties

        /// <summary>Выделенный элемент.</summary>
        public FileModel SelectedFile
        {
            get => _selectedFile;
            set
            {
                if(_selectedFile != value)
                {
                    _selectedFile = value;
                    Notify?.Invoke(_selectedFile);  //вызов события
                }
            }
        }

        /// <summary>Отображаемый список файлов 1 уровня.</summary>
        public FileListModel MainListFiles
        {
            get => _mainListFiles;
            set
            {
                _mainListFiles = value;
                changeMainListNotify?.Invoke(_mainListFiles);
            }
        }

        /// <summary>Отображаемый список файлов 2 уровня.</summary>
        public FileListModel SubListFiles
        {
            get => _subListFiles;
            set
            {
                _subListFiles = value;
                changeSubListNotify?.Invoke(_subListFiles);
            }
        }

        private Settings SettingsControl { get; set; } //свойство для доступа к настройкам

        #endregion

        public Controller()
        {
            SettingsControl = new Settings();   //загружаем настройки
            string lastPath = SettingsControl.GetLastPath();    //последний путь из настроек
            List<string> filesInDir = WorkWithFilesAndDir.GetAllFilesInDir(lastPath);   //список файлов по указанному пути

            MainListFiles = new FileListModel(filesInDir);
            SelectedFile = _mainListFiles.GetFiles()[0];   //устанавливаем выделение на 1 файле
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
            WorkWithFilesAndDir.DeletingFile(filename);
        }

        /// <summary>Создать папку.</summary>
        /// <param name="newDirName">Имя создаваемой папки.</param>
        internal void CreateDirectory(string newDirName)
        {
            WorkWithFilesAndDir.CreatingDirectory(newDirName);
        }

        /// <summary>Переименовать выбранный файл/папку.</summary>
        /// <param name="newName">Новое имя файла.</param>
        internal void Rename(string newName)
        {
            //TODO: заглушка
            //string oldName = _selectedFile.GetFileInfo()[1];
            string currnetPath = @"C:\Users\Scan22\Desktop\разработка\C#\Repositories\Geek\ConsoleFileManager\ConsoleFileManager\test";
            WorkWithFilesAndDir.Renaming(newName, currnetPath);
        }

        /// <summary>Переместить выделенный элемент в указанную директорию.</summary>
        /// <param name="newPath">Директория вставки.</param>
        internal void Move(string newPath)
        {
            string currnetPath = _selectedFile.GetFileInfo()[1];
            currnetPath = @"C:\Users\Scan22\Desktop\разработка\C#\Repositories\Geek\ConsoleFileManager\ConsoleFileManager\test";
            //TODO: проверить получаемые в метод пути
            WorkWithFilesAndDir.Moving(newPath, currnetPath);
        }

        #endregion
    }
}
