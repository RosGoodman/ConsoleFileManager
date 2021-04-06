
using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace ConsoleFileManager.Controllers.Services
{
    /// <summary>Класс описывающий работу с файлами и папками.</summary>
    public class WorkWithFiles
    {
        /// <summary>Удалить файл.</summary>
        /// <param name="filename">Путь к удаляемому файлу/папке.</param>
        internal static void DeletingFile(string filename)
        {
            if (Availability(filename))
            {

            }
        }

        /// <summary>Проверка доступа по указазанному пути к файлу/папке.</summary>
        /// <param name="path">Путь к файлу/папке.</param>
        /// <returns>true/false - доступен/недоступен</returns>
        public static bool Availability(string path)
        {
            FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, path);
            f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, path);
            try
            {
                f2.Demand();
                return true;
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
                return false;
            }

            //if (File.Exists(path))
            //    return true;    //если файл
            //else if (Directory.Exists(path))
            //    return true;    //если папка
            //else
            //    return false;
        }
    }
}
