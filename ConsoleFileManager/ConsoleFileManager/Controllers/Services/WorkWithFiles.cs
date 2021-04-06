
using System.IO;

namespace ConsoleFileManager.Controllers.Services
{
    /// <summary>Класс описывающий работу с файлами и папками.</summary>
    public class WorkWithFiles
    {
        private enum FileType
        {
            Directory,
            File,
            NotFound
        }

        /// <summary>Удалить файл.</summary>
        /// <param name="path">Путь к удаляемому файлу/папке.</param>
        internal static void DeletingFile(string path)
        {
            switch (Exists(path))
            {
                case (FileType.File):
                    FileInfo fileInfo = new FileInfo(path);
                    fileInfo.Delete();
                    break;
                case (FileType.Directory):
                    Directory.Delete(path, true); //true - если директория не пуста удаляем все ее содержимое
                    break;
                case (FileType.NotFound):
                    //запись об ошибке - файл/папка не найден
                    break;
            }
        }

        /// <summary>Создать папку.</summary>
        /// <param name="path">Путь к родительской директории.</param>
        internal static void CreatingDirectory(string newDirName, string rootDirPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(rootDirPath);

            if (!directoryInfo.Exists)
                directoryInfo.Create();

            directoryInfo.CreateSubdirectory(newDirName);
        }

        /// <summary>Переименовать файл/папку.</summary>
        /// <param name="newName">Новое имя.</param>
        /// <param name="oldName">Старое имя.</param>
        internal static void Renaming(string newName, string oldName)
        {
            string newFullName = oldName.Substring(0, oldName.LastIndexOf('\\')) + newName;

            if (File.Exists(newFullName))
            {
                //сообщение об ошибке - файл с таким именем уже существует.
                return;
            }

            if (Exists(oldName) == FileType.File)
            {
                string[] splitStr = oldName.Split(new char[] { '.' });
                string fileFormat = splitStr[splitStr.Length - 1];
                File.Move(oldName, newFullName + fileFormat);
            }
            else if (Exists(oldName) == FileType.Directory)
            {
                File.Move(oldName, newFullName);
            }
        }

        /// <summary>Проверка наличия файла/папки.</summary>
        /// <param name="path">Путь к файлу/папке.</param>
        /// <returns></returns>
        static FileType Exists(string path)
        {
            if (File.Exists(path))
                return FileType.File;    //если файл
            else if (Directory.Exists(path))
                return FileType.Directory;    //если папка
            else
                return FileType.NotFound;
        }
    }
}
