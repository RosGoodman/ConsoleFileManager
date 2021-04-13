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

        private FileModel _selectedFile;        //выделенный файл

        private FileListModel _rootFolder;      //корневая папка для 1 уровня.
        private FileListModel _mainListFiles;   //список файлов 1 уровня
        private FileListModel _subListFiles;    //список файлов 2 уровня
        private List<FileModel> _allActivedFiles;   //список всех активных файлов

        public delegate void ChangeSelectedFileHandler(FileModel selectedFile);
        public event ChangeSelectedFileHandler Notify;          //определение события изменения выделенного элемента

        public delegate void ChangeMainListFiles(List<FileModel> mainMainListFiles);
        public event ChangeMainListFiles ChangeListNotify;  //определение события измененния списка файлов 1 уровня

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

        public List<FileModel> AllActivedFiles
        {
            get => _allActivedFiles;
            set
            {
                if(_allActivedFiles != value)
                {
                    _allActivedFiles = value;
                    ChangeListNotify?.Invoke(_allActivedFiles);
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
                AssemblyFilesIntoList();
            }
        }

        /// <summary>Список файлов 1 уровня.</summary>
        public FileListModel MainListFiles
        {
            get => _mainListFiles;
            set
            {
                _mainListFiles = value;
                AssemblyFilesIntoList();
            }
        }

        /// <summary>Список файлов 2 уровня.</summary>
        public FileListModel SubListFiles
        {
            get => _subListFiles;
            set
            {
                _subListFiles = value;
                AssemblyFilesIntoList();
            }
        }

        //private Settings SettingsControl { get; set; } //свойство для доступа к настройкам

        #endregion

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
            => _settings.ChangeProperty(settingName, value);

        /// <summary>Загрузить сохраненные настройки.</summary>
        public void LoadSettings()
        {
            _settings.LoadSettings();
            string lastPath = _settings.GetLastPath();
            List<string> filesInDir = WorkWithFilesAndDir.GetAllFilesInDir(lastPath);

            RootFolder = new FileListModel(new List<string> { lastPath });
            MainListFiles = new FileListModel(filesInDir);
            SelectedFile = _mainListFiles.GetFiles()[0];
        }

        /// <summary>Сохранить настройки.</summary>
        public void SaveSettings() => _settings.SaveSettings();

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

        /// <summary>Выделить файл выше по списку.</summary>
        internal void SelectTheTopOne()
        {
            ChangeSelectionFile(true, 1);
        }

        /// <summary>Выделить файл ниже по списку.</summary>
        internal void SelectTheLowerOne()
        {
            ChangeSelectionFile(false, 1);
        }

        #endregion

        /// <summary>Изменить selectedFile в заданном направлении.</summary>
        /// <param name="directionUp">Направление движения по списку (true - вврх, false -вниз).</param>
        /// <param name="numbMovementLines">Количество строк смещения выделения.</param>
        private void ChangeSelectionFile(bool directionUp, int numbMovementLines)
        {
            for (int i = 0; i < _allActivedFiles.Count; i++)
            {
                if(_allActivedFiles[i] == SelectedFile)
                {
                    if (directionUp && i >= numbMovementLines)
                    {
                        SelectedFile = _allActivedFiles[i - numbMovementLines];
                        break;
                    }
                    else if (!directionUp && _allActivedFiles.Count - i > numbMovementLines)
                    {
                        SelectedFile = _allActivedFiles[i + numbMovementLines];
                        break;
                    }
                }
            }
        }

        /// <summary>Собрать все файлы в один список.</summary>
        private void AssemblyFilesIntoList()
        {
            List<FileModel> newList = new List<FileModel>();

            AssemblyCicle(newList, _rootFolder, 0);

            if(_mainListFiles != null)
                AssemblyCicle(newList, _mainListFiles, 1, _subListFiles);

            AllActivedFiles = newList;
        }

        /// <summary>Циклы сборки файлов в 1 список.</summary>
        /// <param name="fileListModel"></param>
        /// <param name="deepthFileListModel"></param>
        /// <param name="subFileListModel"></param>
        private void AssemblyCicle(List<FileModel> newList, 
                                    FileListModel fileListModel, 
                                    int deepthFileListModel, 
                                    FileListModel subFileListModel = null)
        {
            //при необходимости можно добавить рекурсию для отображения большего кол-ва раскрытых папок.
            List<FileModel> fileModels = fileListModel.GetFiles();
            List<FileModel> subFileModels = null;

            if (subFileListModel != null)
                subFileModels = subFileListModel.GetFiles();

            for (int i = 0; i < fileModels.Count; i++)
            {
                fileModels[i].DeepthLvl = deepthFileListModel;
                newList.Add(fileModels[i]);

                if (fileModels[i].FolderIsOpen && subFileListModel != null)
                {
                    for (int j = 0; j < subFileModels.Count; j++)
                    {
                        subFileModels[j].DeepthLvl = deepthFileListModel + 1;
                        newList.Add(subFileModels[j]);
                    }
                }
            }
        }
    }
}
