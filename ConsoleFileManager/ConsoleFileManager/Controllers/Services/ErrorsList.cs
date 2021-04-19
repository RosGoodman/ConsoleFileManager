
using System;
using System.IO;

namespace ConsoleFileManager.Controllers.Services
{
    /// <summary>Сохранение ошибок.</summary>
    internal class ErrorsList
    {
        /// <summary>Записать данные об ошибке в файл.</summary>
        /// <param name="errorText">Текст ошибки.</param>
        internal static void WriteErrorInFile(string errorText)
        {
            string filePath = "ErrorsList.txt";

            if (!File.Exists(filePath))
            {
                string createText = errorText + "  ||  " + DateTime.Now.ToString();
                File.WriteAllText(filePath, createText + Environment.NewLine);
            }

            string appendText = errorText + "  ||  " + DateTime.Now.ToString();
            File.AppendAllText(filePath, appendText + Environment.NewLine);
        }
    }
}
