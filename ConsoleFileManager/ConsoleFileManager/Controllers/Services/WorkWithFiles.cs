
namespace ConsoleFileManager.Controllers.Services
{
    /// <summary>Класс описывающий работу с файлами и папками.</summary>
    public class WorkWithFiles
    {
        /// <summary>Удалить файл.</summary>
        /// <param name="filename">Имя удаляемого файла.</param>
        internal static void DeletingFile(string filename)
        {
            System.Console.WriteLine($"file {filename} delete.");
        }
    }
}
