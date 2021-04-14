
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
        internal static void PrintFileList(List<FileModel> fileList, int page, int lineCount)
        {
            Console.Clear();

            int numbStartLine = (page * lineCount) + 1;

            for (int i = numbStartLine; i <= numbStartLine + lineCount -1 ; i++)
            {
                if (fileList[i-1].IsFolder)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                for (int j = 0; j < fileList[i-1].DeepthLvl; j++)
                {
                    Console.Write("    ");
                }

                Console.WriteLine(fileList[i-1].ToString());

                Console.ForegroundColor = ConsoleColor.White;

                if (i == fileList.Count) break;
            }
        }

        /// <summary>Выделить строку в списке.</summary>
        internal static void VisualSelectingFile(FileModel selectedFile, List<FileModel> pageList)
        {
            Console.Clear();

            for (int i = 0; i < pageList.Count; i++)
            {
                Console.SetCursorPosition(Console.CursorLeft, i);
                Console.Write(new String(' ', Console.BufferWidth));

                if (pageList[i].IsFolder) Console.ForegroundColor = ConsoleColor.Yellow;
                if (pageList[i] == selectedFile) Console.BackgroundColor = ConsoleColor.Green;

                for (int j = 0; j < pageList[i].DeepthLvl; j++)
                {
                    Console.Write("    ");
                }

                Console.WriteLine(pageList[i].ToString());

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
