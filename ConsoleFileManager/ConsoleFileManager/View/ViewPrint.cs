
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
        /// <param name="line">Номер строки.</param>
        /// <param name="fileName">Имя файла.</param>
        internal static void VisualSelectingFile(int line, FileModel file, int countLines)
        {
            for (int i = 0; i < countLines; i++)
            {
                string text = string.Empty;
                Console.SetCursorPosition(i, 0);
                //todo: считывание строки для перепечатывания

                if (Console.BackgroundColor == ConsoleColor.Green)
                {
                    text = Console.ReadLine();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(text);
                }
            }

            Console.WriteLine(new string(' ', 50));
            Console.SetCursorPosition(0, line);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(file.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
