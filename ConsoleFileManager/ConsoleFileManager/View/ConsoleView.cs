
using ConsoleFileManager.Controllers.Commands;
using ConsoleFileManager.Controls;
using ConsoleFileManager.Models;
using System;
using System.Collections.Generic;

namespace ConsoleFileManager.View
{
    public class ConsoleView
    {
        private Dictionary<int, Command> _buttons = new Dictionary<int, Command>();     //словарь команд (номер/команда)
        private int _countFilesOnPage = 40;  //кол-во файлов на странице
        private int _numbPage;  //текущая страница

        public ConsoleView(Controller controller)
        {
            controller.Notify += UpdateLists;
            controller.PageChangeNotify += ChangePageNumb;
            controller.LoadSettings();
        }

        /// <summary>Установить команду в соответствии с номером.</summary>
        /// <param name="number">Номер команды.</param>
        /// <param name="com">Команда.</param>
        public void SetCommand(int number, Command com)
        {
            _buttons[number] = com;
        }

        /// <summary>Вызов комады.</summary>
        /// <param name="number">Номер команды.</param>
        /// <param name="param">Параметр команды (если есть).</param>
        private void PressButton(int number, string param = "")
        {
            _buttons[number].Execute(param);
        }

        private void PressUndoButton(int number, string param = "")
        {
            _buttons[number].Undo(param);
        }

        /// <summary>Обработка нажатия клавиш.</summary>
        public void Explore()
        {
            bool exit = false;

            while (!exit)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.Enter:
                            PressButton(1); //ChangeDirectoryOrRunProcessCommand();
                            break;
                        case ConsoleKey.F5:
                            ViewPrint.HelpMessage("Выберите директорию для вставки, за тем еще раз нажмите F5.");
                            PressButton(4); //CopyCommand();
                            break;
                        case ConsoleKey.F6:
                            ViewPrint.HelpMessage("Выберите директорию для вставки, за тем еще раз нажмите F6.");
                            PressButton(5); //MoveCommand();
                            break;
                        case ConsoleKey.F7:
                            PressButton(6, ViewPrint.ReadParamString()); //CreateDirectoryCommand();
                            break;
                        case ConsoleKey.F8:
                            PressButton(7, ViewPrint.ReadParamString());  //RenameCommand();
                            break;
                        case ConsoleKey.F9:
                            if(Confirmation()) PressButton(8); //DeleteCommand();
                            break;
                        case ConsoleKey.F10:
                            PressButton(9); //exitCommand
                            exit = true;
                            Console.ResetColor();
                            Console.Clear();
                            break;
                        case ConsoleKey.DownArrow:
                            PressButton(10);    //SelectTheLowerOneCommand
                            break;
                        case ConsoleKey.UpArrow:
                            PressButton(11);    //SelectTheTopOneCommand
                            break;
                        case ConsoleKey.End:
                            PressButton(12);    //SelectingTheLastFileOnPageCommand
                            break;
                        case ConsoleKey.Home:
                            PressButton(13);    //SelectingTheFirstFileOnPageCommand
                            break;
                        case ConsoleKey.PageDown:
                            PressUndoButton(14);    //NextPageCommand
                            break;
                        case ConsoleKey.PageUp:
                            PressButton(14);    //PreviousePageCommand
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>Изменить номер страницы.</summary>
        /// <param name="newPageNumb">Новый номер страницы.</param>
        private void ChangePageNumb(int newPageNumb)
        {
            _numbPage = newPageNumb;
        }

        /// <summary>Запрос на подтверждение действия.</summary>
        /// <returns>true/false - подтверждение/отмена.</returns>
        internal static bool Confirmation()
        {
            ViewPrint.ConfirmationPrint();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.N:
                            return false;
                        case ConsoleKey.Y:
                            return true;
                    }
                }
            }
        }

        /// <summary>Изменение выделенного файла.</summary>
        /// <param name="file">Новый выделенный файл.</param>
        /// <param name="fileList">Список файлов.</param>
        private void UpdateLists(FileModel file, List<FileModel> fileList)
        {
            List<FileModel> pageList = new List<FileModel>();
            int firstOnPage = _numbPage * _countFilesOnPage;

            for (int i = firstOnPage; i < firstOnPage + _countFilesOnPage; i++)
            {
                if (i + 1 > fileList.Count) break;

                //добавляем файлы в станицу
                pageList.Add(fileList[i]);
            }

            Console.Clear();
            ViewPrint.PrintButtonsInfo();
            ViewPrint.PrintNumbPage(_numbPage);
            VisualSelectingFile(file, pageList);

            if (file != null)
            {
                List<string> fileInfo = file.GetFileInfo();
                ViewPrint.PrintFileInfo(fileInfo, _countFilesOnPage);
            }
        }

        /// <summary>Вывести страницу в консоль.</summary>
        /// <param name="selectedFile">Текущий выделенный файл/папка.</param>
        /// <param name="pageList">Выводимая страница.</param>
        private void VisualSelectingFile(FileModel selectedFile, List<FileModel> pageList)
        {
            bool isSelected = false;

            for (int i = 0; i < pageList.Count; i++)
            {
                string printingString = string.Empty;
                int cursorPos = i+5;

                if (pageList[i] == selectedFile) isSelected = true;

                for (int j = 0; j < pageList[i].DeepthLvl; j++) //отступ в зависимости от уровня списка
                {
                    printingString += "    ";
                }

                printingString += pageList[i].ToString();
                ViewPrint.PrintFileOnPage(printingString, cursorPos, isSelected, pageList[i].IsFolder); //печать в консоль
                isSelected = false;
            }
        }
    }
}
