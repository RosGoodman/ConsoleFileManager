
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
        #region ControllerMoethods  

        /// <summary>Открыть выбранную директорию.</summary>
        internal static void OpenFolder()
        {
            List<FileModel> rootList = _rootFolder.GetFiles();

            if (_selectedFile == rootList[0])    //если открываемая папка не является корнем
            {
                //todo: условие, что корень не является диском (C:, D: и т.д.)

                //вверх по корневой папке
                ChangeRootDir(false);
                return;
            }

            if (!_selectedFile.FolderIsOpen)    //если папка не открыта
            {
                List<FileModel> mainList = _mainListFiles.GetFiles();

                if (mainList.Contains(_selectedFile))
                    OpenFolderInMainList(mainList); //если открываемая папка в mainListFiles
                else
                    ChangeRootDir(true);          // если открываемая папка в subListFiles
            }
            else
            {
                //закрываем открытую
                _subListFiles = null;
                SelectedFile.FolderIsOpen = false;
            }
        }

        /// <summary>Открываем папку находящуюся в mainListFiles.</summary>
        /// <param name="mainList">Список файлов 1 уровня.</param>
        private void OpenFolderInMainList(List<FileModel> mainList)
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

            List<string> filesInOpenDir = WorkWithFilesAndDir.GetAllFilesInDir(_selectedFile.FilePath); //получаем список файлов в папке
            _subListFiles = new FileListModel(filesInOpenDir);  //записываем в список 2 уровня
            SelectedFile.FolderIsOpen = true;   //изменяем св-во, срабатывает событие
        }

        /// <summary>Открыть папку находящуюся в subListFiles.</summary>
        /// <param name="selectedIsRoot">true - Сделать выбранную папку корневой, false - сделать parent коневой.</param>
        private void ChangeRootDir(bool selectedIsRoot)
        {
            //открываем папку в subList
            //заменяем rootList
            DirectoryInfo di = new DirectoryInfo(_selectedFile.FilePath);
            List<string> newRootList;

            if (selectedIsRoot)
                newRootList = new List<string>() { di.FullName };
            else
            {
                DirectoryInfo parentDir = di.Parent;
                newRootList = new List<string>() { parentDir.FullName };
            }

            _rootFolder = new FileListModel(newRootList);

            //флаг об открытии папки selectedFile
            List<FileModel> rootFile = _rootFolder.GetFiles();
            _selectedFile = rootFile[0];
            _selectedFile.FolderIsOpen = true;

            //новый список mainLIst
            List<string> newMainList = WorkWithFilesAndDir.GetAllFilesInDir(_selectedFile.FilePath);
            _mainListFiles = new FileListModel(newMainList);

            //обновление subListFiles
            SubListFiles = null;
        }

        /// <summary>Обновить номер страницы.</summary>
        /// <param name="newList">Список всех активных файлов.</param>
        private static void UpdatePageNumer(List<FileModel> newList)
        {
            int numbStr = 0;
            for (int i = 0; i < newList.Count; i++)
            {
                if (newList[i] == _selectedFile)
                {
                    numbStr = i;
                    break;
                }
            }
            NumbPage = numbStr / _settings.GetCountStrInPage();
        }

        /// <summary>Запустить выбранный процесс.</summary>
        internal static void RuningProcess()
        {
            Process.Start(_selectedFile.FilePath);
        }

        /// <summary>Изменить selectedFile в заданном направлении.</summary>
        /// <param name="directionUp">Направление движения по списку (true - вврх, false -вниз).</param>
        /// <param name="numbMovementLines">Количество строк смещения выделения.</param>
        internal static void ChangeSelectionFile(Controller controller, bool directionUp, int numbMovementLines)
        {
            for (int i = 0; i < controller.AllActivedFiles.Count; i++)
            {
                if (controller.AllActivedFiles[i] == controller.SelectedFile)
                {
                    if (directionUp && i >= numbMovementLines)
                    {
                        controller.NumbPage = (i - numbMovementLines) / controller.Settings.GetCountStrInPage();    //изменяем номер страницы
                        controller.SelectedFile = controller.AllActivedFiles[i - numbMovementLines];
                        break;
                    }
                    else if (!directionUp && controller.AllActivedFiles.Count - i > numbMovementLines)
                    {
                        int numb = controller._settings.GetCountStrInPage();
                        controller.NumbPage = (i + numbMovementLines) / numb;    //изменяем номер страницы
                        controller.SelectedFile = controller.AllActivedFiles[i + numbMovementLines];
                        break;
                    }
                }
            }
        }

        /// <summary>Собрать все файлы в один список.</summary>
        internal static void AssemblyFilesIntoList(Controller controller)
        {
            List<FileModel> newList = new List<FileModel>();

            AssemblyCicle(newList, controller.RootFolder, 0);

            if (controller.MainListFiles != null)
                AssemblyCicle(newList, controller.MainListFiles, 1, controller.SubListFiles);

            //пересчет номера страницы
            UpdatePageNumer(newList);

            controller.AllActivedFiles = newList;
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

        #endregion
    }
}
