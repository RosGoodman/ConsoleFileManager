
using ConsoleFileManager.Controllers.Services;
using ConsoleFileManager.Controls;
using ConsoleFileManager.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleFileManager.Controllers
{
    /// <summary>Класс описывающий методы обрабатывающий данные проходящие через контроллер.</summary>
    internal class ControllerMethods
    {
        private static Controller _controller;
        public ControllerMethods(Controller controller)
        {
            _controller = controller;
        }

        #region ControllerMoethods  

        /// <summary>Открыть выбранную директорию.</summary>
        internal static void OpenFolder()
        {
            List<FileModel> rootList = _controller.RootFolder.GetFiles();

            if (_controller.SelectedFile == rootList[0])    //если открываемая папка не является корнем
            {
                //todo: условие, что корень не является диском (C:, D: и т.д.)

                //вверх по корневой папке
                ChangeRootDir(false);
                return;
            }

            if (!_controller.SelectedFile.FolderIsOpen)    //если папка не открыта
            {
                List<FileModel> mainList = _controller.MainListFiles.GetFiles();

                if (mainList.Contains(_controller.SelectedFile))
                    OpenFolderInMainList(mainList); //если открываемая папка в mainListFiles
                else
                    ChangeRootDir(true);          // если открываемая папка в subListFiles
            }
            else
            {
                //закрываем открытую
                _controller.SubListFiles = null;
                _controller.SelectedFile.FolderIsOpen = false;
            }
        }

        /// <summary>Открываем папку находящуюся в mainListFiles.</summary>
        /// <param name="mainList">Список файлов 1 уровня.</param>
        private static void OpenFolderInMainList(List<FileModel> mainList)
        {
            //открываем новую папку, закрываем (если открыта) другую
            for (int i = 0; i < mainList.Count; i++)
            {
                if (mainList[i].FolderIsOpen)
                {
                    mainList[i].FolderIsOpen = false; //флаг - папка закрыта.
                    break;
                }
            }

            List<string> filesInOpenDir = WorkWithFilesAndDir.GetAllFilesInDir(_controller.SelectedFile.FilePath); //получаем список файлов в папке
            _controller.SubListFiles = new FileListModel(filesInOpenDir);  //записываем в список 2 уровня
            _controller.SelectedFile.FolderIsOpen = true;   //изменяем св-во, срабатывает событие
        }

        /// <summary>Открыть папку находящуюся в subListFiles.</summary>
        /// <param name="selectedIsRoot">true - Сделать выбранную папку корневой, false - сделать parent коневой.</param>
        private static void ChangeRootDir(bool selectedIsRoot)
        {
            //открываем папку в subList
            //заменяем rootList
            DirectoryInfo di = new DirectoryInfo(_controller.SelectedFile.FilePath);
            List<string> newRootList;

            if (selectedIsRoot)
                newRootList = new List<string>() { di.FullName };
            else
            {
                DirectoryInfo parentDir = di.Parent;
                newRootList = new List<string>() { parentDir.FullName };
            }

            _controller.RootFolder = new FileListModel(newRootList);

            //флаг об открытии папки selectedFile
            List<FileModel> rootFile = _controller.RootFolder.GetFiles();
            _controller.SelectedFile = rootFile[0];
            _controller.SelectedFile.FolderIsOpen = true;

            //новый список mainLIst
            List<string> newMainList = WorkWithFilesAndDir.GetAllFilesInDir(_controller.SelectedFile.FilePath);
            _controller.MainListFiles = new FileListModel(newMainList);

            //обновление subListFiles
            _controller.SubListFiles = null;
        }

        /// <summary>Обновить номер страницы.</summary>
        /// <param name="newList">Список всех активных файлов.</param>
        private static void UpdatePageNumber(List<FileModel> newList)
        {
            int numbStr = 0;
            for (int i = 0; i < newList.Count; i++)
            {
                if (newList[i] == _controller.SelectedFile)
                {
                    numbStr = i;
                    break;
                }
            }
            _controller.NumbPage = numbStr / _controller.Settings.GetCountStrInPage();
        }

        /// <summary>Запустить выбранный процесс.</summary>
        internal static void RuningProcess()
        {
            Process.Start(_controller.SelectedFile.FilePath);
        }

        /// <summary>Изменить selectedFile в заданном направлении.</summary>
        /// <param name="directionUp">Направление движения по списку (true - вврх, false -вниз).</param>
        /// <param name="numbMovementLines">Количество строк смещения выделения.</param>
        internal static void ChangeSelectionFile(bool directionUp, int numbMovementLines)
        {
            for (int i = 0; i < _controller.AllActivedFiles.Count; i++)
            {
                if (_controller.AllActivedFiles[i] == _controller.SelectedFile)
                {
                    if (directionUp && i >= numbMovementLines)  //если смещение вверх
                    {
                        _controller.NumbPage = (i - numbMovementLines) / _controller.Settings.GetCountStrInPage();    //изменяем номер страницы
                        _controller.SelectedFile = _controller.AllActivedFiles[i - numbMovementLines];
                        break;
                    }
                    else if (!directionUp) //если смещение вниз
                    {
                        int countOnPage = _controller.Settings.GetCountStrInPage();
                        

                        if (i + numbMovementLines > _controller.AllActivedFiles.Count - 1)//кол-во элементов на странице меньше макс. возможного
                        {
                            _controller.NumbPage = _controller.AllActivedFiles.Count / countOnPage;    //изменяем номер страницы
                            _controller.SelectedFile = _controller.AllActivedFiles[_controller.AllActivedFiles.Count - 1];
                        }
                        else
                        {
                            _controller.NumbPage = (i + numbMovementLines) / countOnPage;    //изменяем номер страницы
                            _controller.SelectedFile = _controller.AllActivedFiles[i + numbMovementLines];
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>Собрать все файлы в один список.</summary>
        internal static void AssemblyFilesIntoList()
        {
            List<FileModel> newList = new List<FileModel>();

            AssemblyCicle(newList, _controller.RootFolder, 0);

            if (_controller.MainListFiles != null)
                AssemblyCicle(newList, _controller.MainListFiles, 1, _controller.SubListFiles);

            //пересчет номера страницы
            UpdatePageNumber(newList);

            _controller.AllActivedFiles = newList;
        }

        /// <summary>Циклы сборки файлов в 1 список.</summary>
        /// <param name="fileListModel"></param>
        /// <param name="deepthFileListModel"></param>
        /// <param name="subFileListModel"></param>
        private static void AssemblyCicle(List<FileModel> newList,
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

        /// <summary>Получить индекс выделенного в списке файла.</summary>
        /// <param name="fileModels">Список поиска.</param>
        /// <param name="selectedItem">Искомый элемент.</param>
        /// <returns>Индекс.</returns>
        internal static int GetIndexSelectedFile(List<FileModel> fileModels, FileModel selectedItem)
        {
            for (int i = 0; i < fileModels.Count; i++)
            {
                if (fileModels[i] == selectedItem) return i;
            }
            return -1;
        }

        #endregion
    }
}
