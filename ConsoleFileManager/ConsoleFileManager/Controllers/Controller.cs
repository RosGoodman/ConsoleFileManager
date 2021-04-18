using ConsoleFileManager.Controllers;
using ConsoleFileManager.Controllers.Services;
using ConsoleFileManager.Controllers.Settings;
using ConsoleFileManager.Models;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Controls
{
    public class Controller
    {
        private Settings _settings = new Settings(); //инициализация класса настроек

        private int _numbPage;  //номер текущей страницы

        private FileModel _selectedFile;        //выделенный файл
        private FileModel _movingFile;          //копируемый или вырезаемый файл

        private FileListModel _rootFolder;      //корневая папка для 1 уровня.
        private FileListModel _mainListFiles;   //список файлов 1 уровня
        private FileListModel _subListFiles;    //список файлов 2 уровня
        private List<FileModel> _allActivedFiles;   //список всех активных файлов

        public delegate void ChangeFileHandler(FileModel selectedFile, List<FileModel> listFiles);
        public event ChangeFileHandler Notify;          //определение события изменения в списке

        public delegate void ChangePageHandler(int numbPage);
        public event ChangePageHandler PageChangeNotify;          //определение события изменения номера страницы


        public Controller()
        {
            ControllerMethods _controllerMethods = new ControllerMethods(this);
        }

        ////////////////////////////////////////////////////////////////////////////////////////

        #region Properties

        /// <summary>Настройки программы.</summary>
        internal Settings Settings
        {
            get => _settings;
        }

        /// <summary>Номер текущей страницы.</summary>
        public int NumbPage
        {
            get => _numbPage;
            set
            {
                if(_numbPage != value)
                {
                    _numbPage = value;
                    PageChangeNotify?.Invoke(_numbPage);
                }
            }
        }

        /// <summary>Выделенный элемент.</summary>
        public FileModel SelectedFile
        {
            get => _selectedFile;
            set
            {
                if(_selectedFile != value)
                {
                    _selectedFile = value;
                    Notify?.Invoke(_selectedFile, _allActivedFiles);  //вызов события
                }
            }
        }

        /// <summary>Упорядоченный для вывода список всех активных файлов.</summary>
        public List<FileModel> AllActivedFiles
        {
            get => _allActivedFiles;
            set
            {
                if(_allActivedFiles != value)
                {
                    _allActivedFiles = value;
                    Notify?.Invoke(_selectedFile, _allActivedFiles);
                }
            }
        }

        /// <summary>Корневая папка.</summary>
        public FileListModel RootFolder
        {
            get => _rootFolder;
            set
            {
                _rootFolder = value;
                SaveSettings();
                ControllerMethods.AssemblyFilesIntoList();
            }
        }

        /// <summary>Список файлов 1 уровня.</summary>
        public FileListModel MainListFiles
        {
            get => _mainListFiles;
            set
            {
                _mainListFiles = value;
                ControllerMethods.AssemblyFilesIntoList();
            }
        }

        /// <summary>Список файлов 2 уровня.</summary>
        public FileListModel SubListFiles
        {
            get => _subListFiles;
            set
            {
                _subListFiles = value;
                ControllerMethods.AssemblyFilesIntoList();
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////

        #region SettingsControl interface

        /// <summary>Установить новое значение параметра.</summary>
        /// <param name="settingName">Наименование параметра.</param>
        /// <param name="value">Новое значение параметра.</param>
        public void SetNewSettingValue(Settings.PropNames settingName, string value)
            => _settings.ChangeProperty(settingName, value);

        /// <summary>Загрузить сохраненные настройки.</summary>
        public void LoadSettings()
        {
            _settings.LoadSettings();
            string lastPath = _settings.GetLastPath();
            List<string> filesInDir = WorkWithFilesAndDir.GetAllFilesInDir(lastPath);

            RootFolder = new FileListModel(new List<string> { lastPath });
            MainListFiles = new FileListModel(filesInDir);
            SelectedFile = _allActivedFiles[0];
        }

        /// <summary>Сохранить настройки.</summary>
        public void SaveSettings()
        {
            List<FileModel> root = _rootFolder.GetFiles();
            Settings.ChangeProperty(Settings.PropNames.LastPath, root[0].FilePath); //изменяем последний путь в настройках

            _settings.SaveSettings();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////

        #region commands

        /// <summary>Удалить файл.</summary>
        /// <param name="filename">Имя удаляемого файла.</param>
        internal void DeletingFile()
        {
            string currnetPath = SelectedFile.FilePath;
            WorkWithFilesAndDir.DeletingFile(currnetPath);

            List<FileModel> thisRoot;

            //поднимаемся вверх по директории
            if (SelectedFile.DeepthLvl == 2)
                thisRoot = _mainListFiles.GetFiles();
            else
                thisRoot = _rootFolder.GetFiles();

            foreach (var file in thisRoot)
            {
                if (file.FolderIsOpen) file.FolderIsOpen = false; 
            }

            SelectedFile = thisRoot[0];
            
            ChangeDirOrRunProcess(false);    //поднимаемся вверх по директории

            Settings.LoadSettings();
        }

        /// <summary>Создать папку.</summary>
        /// <param name="newDirName">Имя создаваемой папки.</param>
        internal void CreateDirectory(string newDirName)
        {
            string currnetPath = SelectedFile.FilePath;
            string newFullName = currnetPath + "\\" + newDirName;
            WorkWithFilesAndDir.CreatingDirectory(newFullName);

            List<string> newFiles = new List<string> { newFullName };

            switch (SelectedFile.DeepthLvl)
            {
                case 0:
                    if(MainListFiles != null) MainListFiles.AddFilesInList(newFiles);
                    break;
                case 1:
                    if(SubListFiles != null) SubListFiles.AddFilesInList(newFiles);
                    break;
            };

            ControllerMethods.AssemblyFilesIntoList();
        }

        /// <summary>Переименовать выбранный файл/папку.</summary>
        /// <param name="newName">Новое имя файла.</param>
        internal void Rename(string newName)
        {
            string currnetPath = SelectedFile.FilePath;
            string newFullName = currnetPath.Substring(0, currnetPath.LastIndexOf('\\')) + "\\" + newName;

            WorkWithFilesAndDir.Moving(newFullName, currnetPath);
            SelectedFile.FilePath = newFullName;

            Notify?.Invoke(_selectedFile, _allActivedFiles);  //вызов события
        }

        /// <summary>Переместить выделенный элемент в указанную директорию.</summary>
        /// <param name="newPath">Директория вставки.</param>
        internal void Move()
        {
            if (_movingFile == null)
                _movingFile = _selectedFile;
            else
            {
                string currnetPath = _movingFile.FilePath;
                WorkWithFilesAndDir.Moving(_selectedFile.FilePath + "\\" + _movingFile.ToString(), currnetPath);
                _movingFile = null;

                LoadSettings();
            }
        }

        /// <summary>Выделить файл выше по списку.</summary>
        internal void SelectTheTopOne()
        {
            ControllerMethods.ChangeSelectionFile(true, 1);
        }

        /// <summary>Выделить файл ниже по списку.</summary>
        internal void SelectTheLowerOne()
        {
            ControllerMethods.ChangeSelectionFile(false, 1);
        }

        /// <summary>Выделить первый файл на странице.</summary>
        internal void SelectFirstOnPage()
        {
            int countStepUp = ControllerMethods.GetIndexSelectedFile(_allActivedFiles, _selectedFile) % _settings.GetCountStrInPage();
            ControllerMethods.ChangeSelectionFile(true, countStepUp);
        }

        /// <summary>Выделить последний файл на странице.</summary>
        internal void SelectLastOnPage()
        {
            int count = _settings.GetCountStrInPage();
            int index = ControllerMethods.GetIndexSelectedFile(_allActivedFiles, _selectedFile);
            int countStepUp = count - (index % count) - 1;

            ControllerMethods.ChangeSelectionFile(false, countStepUp);
        }

        /// <summary>Открыть папку или запустить процесс.</summary>
        internal void ChangeDirOrRunProcess(bool open = true)
        {
            if (_selectedFile.IsFolder)
            {
                if(open)
                    ControllerMethods.OpenFolder();
            }
            else
                ControllerMethods.RuningProcess();

            ControllerMethods.AssemblyFilesIntoList();    //объединяем спискм в один
        }

        /// <summary>Перейти на страницу вверх.</summary>
        internal void PageUp()
        {
            SelectFirstOnPage();
            SelectTheTopOne();
            SelectFirstOnPage();
        }

        /// <summary>Перейти на страницу вниз.</summary>
        internal void PageDown()
        {
            SelectLastOnPage();
            SelectTheLowerOne();
        }

        /// <summary>Завершить работу с программой.</summary>
        internal void ExitProgram()
        {
            SaveSettings();
        }

        /// <summary>Копировать выбранный файл.</summary>
        internal void Copy()
        {
            if (_movingFile == null)
                _movingFile = _selectedFile;
            else
            {
                string newDir = _selectedFile.FilePath + "\\" + _movingFile.ToString();
                if (_movingFile.IsFolder)
                {

                    CreateDirectory(_movingFile.ToString());
                }

                string currnetPath = _movingFile.FilePath;
                WorkWithFilesAndDir.CopyDir(currnetPath, newDir);
                _movingFile = null;

                LoadSettings();
            }
        }

        #endregion
    }
}
