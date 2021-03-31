using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Controllers.Services
{
    /// <summary>Класс описывающий работу с файлами json.</summary>
    internal class JsonFile
    {
        /// <summary>Записать параметры в файл.</summary>
        /// <param name="dict">Словарь с настройками (имя\значение).</param>
        /// <param name="fileName">Имя файла.</param>
        internal static void WriteSettingsInJson(Dictionary<string,string> dict, string fileName)
        {
            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        /// <summary>Прочитать параметры из файла.</summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Словарь с настройками (имя\значение).</returns>
        internal static Dictionary<string,string> ReadSettingsFromJson(string fileName)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                string json = File.ReadAllText(fileName);
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return dict;
            }
            catch
            {
                string err = $"Файл {fileName} не найден.";     //для записи ошибок
            }
            
            return dict;
        }
    }
}
