
using System.IO;

namespace ConsoleFileManager.Controllers.Services
{
    /// <summary>Класс описывающий работу с файлами и папками.</summary>
    public class WorkWithFilesAndDir
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
        /// <param name="newDirName">Имя новой папки.</param>
        internal static void CreatingDirectory(string newDirName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(newDirName);

            if (!directoryInfo.Exists)
                directoryInfo.Create();
        }

        /// <summary>Переименовать файл/папку.</summary>
        /// <param name="newName">Новое имя.</param>
        /// <param name="oldName">Старое имя.</param>
        internal static void Renaming(string newName, string oldName)
        {
            string newFullName = oldName.Substring(0, oldName.LastIndexOf('\\')) + "\\" + newName;

            Moving(newFullName, oldName);
        }

        /// <summary>Изменить путь к файлу.</summary>
        /// <param name="newPath">Новый путь.</param>
        /// <param name="currentPath">Текущий путь.</param>
        internal static void Moving(string newPath, string currentPath)
        {
            if (File.Exists(newPath))
            {
                //сообщение об ошибке - файл с таким именем уже существует.
                return;
            }

            if (Exists(currentPath) == FileType.File)
            {
                string[] splitStr = currentPath.Split(new char[] { '.' });
                string fileFormat = splitStr[splitStr.Length - 1];
                File.Move(currentPath, newPath + fileFormat);
            }
            else if (Exists(currentPath) == FileType.Directory)
            {
                Directory.Move(currentPath, newPath);
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

        /// <summary>Получить список всех файлов указанной директории.</summary>
        /// <param name="directory">Рассматриваемая директория.</param>
        /// <returns>Массив всех файлов.</returns>
        internal static string[] GetAllFilesInDir(string directory)
        {
            string[] files = Directory.GetFiles(directory);
            return files;
        }
    }
}
