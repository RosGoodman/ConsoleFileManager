
using ConsoleFileManager.Models;
using System;
using System.Collections.Generic;

namespace ConsoleFileManager.View
{
    /// <summary>Класс описывающий вывод данных в консоль.</summary>
    internal class ViewPrint
    {
        /// <summary>Вывести список файлов в консоль.</summary>
        /// <param name="fileList">Список файлов 1 уровня.</param>
        internal static void PrintFileList(List<FileModel> fileList, int page, int pageCount)
        {
            Console.Clear();

            for (int i = 0; i < fileList.Count; i++)
            {
                if (fileList[i].IsFolder)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                for (int j = 0; j < fileList[i].DeepthLvl; j++)
                {
                    Console.Write("    ");
                }

                Console.WriteLine(fileList[i].ToString());

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        internal static void VisualSelectingFile(FileModel selectedFile)
        {

        }
    }
}
