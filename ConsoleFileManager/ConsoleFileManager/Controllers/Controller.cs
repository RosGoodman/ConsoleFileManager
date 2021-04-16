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
        private ControllerMethods _controllerMethods;

        private int _numbPage;  //номер текущей страницы

        private FileModel _selectedFile;        //выделенный файл

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
            _controllerMethods = new ControllerMethods(this);
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
        public void SetNewSettingValue(string settingName, string value)
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
        public void SaveSettings() => _settings.SaveSettings();

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////

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
        internal void ChangeDirOrRunProcess()
        {
            if (_selectedFile.IsFolder)
                ControllerMethods.OpenFolder();
            else
                ControllerMethods.RuningProcess();

            ControllerMethods.AssemblyFilesIntoList();    //объединяем спискм в один
        }

        internal void PageUp()
        {
            NumbPage++;
        }

        internal void PageDown()
        {
            NumbPage--;
        }

        #endregion
    }
}
