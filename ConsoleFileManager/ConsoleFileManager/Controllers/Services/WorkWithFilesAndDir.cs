
using System;
using System.Collections.Generic;
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
                    ErrorsList.WriteErrorInFile("Удаляемый файл не найден.");
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

        /// <summary>Изменить путь к файлу.</summary>
        /// <param name="newPath">Новый путь.</param>
        /// <param name="currentPath">Текущий путь.</param>
        internal static void Moving(string newPath, string currentPath)
        {
            if (File.Exists(newPath))
            {
                ErrorsList.WriteErrorInFile("Перемещение файла: файл с таким именем уже существует.");
                return;
            }

            if (Exists(currentPath) == FileType.File)
            {
                try
                {
                    string[] splitStr = currentPath.Split(new char[] { '.' });
                    string fileFormat = splitStr[splitStr.Length - 1];
                    File.Move(currentPath, newPath);
                }
                catch(Exception e) { ErrorsList.WriteErrorInFile(e.Message); }
            }
            else if (Exists(currentPath) == FileType.Directory)
            {
                try
                {
                    Directory.Move(currentPath, newPath);
                }
                catch(Exception e) { ErrorsList.WriteErrorInFile(e.Message); }
            }
        }

        /// <summary>Копировать файл по указанному пути.</summary>
        /// <param name="newPath">Новый путь.</param>
        /// <param name="currentPath">Текущий путь.</param>
        internal static void CopyFile(string currentPath, string newPath)
        {
            try
            {
                File.Copy(currentPath, newPath, true);
            }
            catch(Exception e)
            {
                ErrorsList.WriteErrorInFile(e.Message);
            }
        }

        /// <summary>Копировать папку в указанную директорию.</summary>
        /// <param name="FromDir">Директория копирования.</param>
        /// <param name="ToDir">Директория вставки.</param>
        internal static void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);   //создание копируемой директории.
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                try
                {
                    File.Copy(s1, s2);
                }
                catch (Exception e) { ErrorsList.WriteErrorInFile(e.Message); }
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                try
                {
                    CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
                }
                catch (Exception e) { ErrorsList.WriteErrorInFile(e.Message); }
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

        /// <summary>Получить список логических дисков.</summary>
        /// <returns>Список дисов.</returns>
        internal static List<string> GetAllDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<string> allDrivesList = new List<string>();

            foreach (DriveInfo d in allDrives)
            {
                allDrivesList.Add(d.Name);
            }
            return allDrivesList;
        }

        /// <summary>Получить список всех файлов указанной директории.</summary>
        /// <param name="directory">Рассматриваемая директория.</param>
        /// <returns>Массив всех файлов.</returns>
        internal static List<string> GetAllFilesInDir(string directory)
        {
            string[] dir = null;
            string[] files = null;

            try
            {
                dir = Directory.GetDirectories(directory);
                files = Directory.GetFiles(directory);
            }
            catch(Exception e) { ErrorsList.WriteErrorInFile(e.Message); }
            
            List<string> filesAndDir = new List<string>();

            if(dir != null) AddInList(filesAndDir, dir);
            if(files != null) AddInList(filesAndDir, files);

            return filesAndDir;
        }

        /// <summary>Добавить массив в список.</summary>
        /// <param name="list">Список для добавления.</param>
        /// <param name="addedArr">Добавляемый массив.</param>
        private static void AddInList(List<string> list, string[] addedArr)
        {
            for (int i = 0; i < addedArr.Length; i++)
            {
                list.Add(addedArr[i]);
            }
        }
    }
}
