
using ConsoleFileManager.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.View
{
    /// <summary>Класс описывающий вывод данных в консоль.</summary>
    internal class ViewPrint
    {
        /// <summary>Вывести списки файлов в консоль.</summary>
        /// <param name="ListFirstLvl">Список файлов 1 уровня.</param>
        /// <param name="ListSecondLvl">Список файлов 2 уровня.</param>
        internal static void PrintFileList(List<FileModel> ListFirstLvl, List<FileModel> ListSecondLvl = null)
        {
            string root = string.Empty;
            if (ListSecondLvl != null)
            {
                FileInfo fileInfo = new FileInfo(ListSecondLvl[0].FilePath);
                root = fileInfo.Directory.Name;
            }

            Printing(ListFirstLvl, ListSecondLvl, false, root);
        }

        /// <summary>Вывод списков в консоль в соответствии с уровнем списка.</summary>
        /// <param name="ListFirstLvl">Список файлов 1 уровня.</param>
        /// <param name="ListSecondLvl">Список файлов 2 уровня.</param>
        /// <param name="root">Корневая папка для списка 2 уровня.</param>
        /// <param name="tab">Отступ.</param>
        private static void Printing(List<FileModel> ListFirstLvl, List<FileModel> ListSecondLvl, bool tab, string root = "")
        {
            Console.Clear();

            foreach (FileModel file in ListFirstLvl)
            {
                if (root == file.ToString())
                    Printing(ListSecondLvl, ListSecondLvl, true, string.Empty);

                if (file.IsFolder)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;

                if (tab)
                    Console.Write("    ");

                Console.WriteLine(file.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void VisualSelectingFile(FileModel selectedFile)
        {

        }
    }
}
