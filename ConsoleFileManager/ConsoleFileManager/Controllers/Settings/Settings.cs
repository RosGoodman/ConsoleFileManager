
using ConsoleFileManager.Controllers.Services;
using ConsoleFileManager.Models;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.Controllers.Settings
{
    /// <summary>Класс описывающий действия с настройками.</summary>
    internal class Settings
    {
        private string _settingsFileName = "Settings.json";  //имя файла с настройками
        private List<PropertyBase> _propList = new List<PropertyBase>();  //список экземпляров классов
        private Dictionary<string, string> _settings;    //настройки программы (параметр\значение). Для сериадизации.

        /// <summary>Список экземпляров настроек.</summary>
        internal List<PropertyBase> PropList
        {
            get => _propList;
            set { _propList = value; }
        }

        /// <summary>Получить путь к последней папке.</summary>
        /// <returns>Путь к папке.</returns>
        internal string GetLastPath()
        {
            foreach(PropertyBase prop in _propList)
            {
                if (prop.propName == "LastPath")
                    return prop.propValue;
            }
            return string.Empty;
        }

        public Settings()
        {
            _propList.Add(new LastPathProperty());
            _propList.Add(new WindowWidthProperty());
            _propList.Add(new WindowHeightProperty());
            _propList.Add(new StringCountProperty());
            LoadSettings();
        }

        /// <summary>Изменить настройки.</summary>
        /// <param name="propName">Наименование параметра.</param>
        /// <param name="value">Значение параметра.</param>
        internal void ChangeProperty(string propName, string value)
        {
            foreach(PropertyBase prop in _propList)
            {
                if (prop.propName == propName)
                {
                    prop.propValue = value;
                    break;
                }
            }
        }

        /// <summary>Загрузть настрйки.</summary>
        internal void LoadSettings()
        {
            _settings = JsonFile.ReadSettingsFromJson(_settingsFileName);   //загружаем из файла

            if (_settings.Count == 0) return;

            foreach(PropertyBase prop in _propList) //присваиваем экземплярам
            {
                prop.LoadProperty(_settings);
            }
        }

        /// <summary>Сохранить настройки.</summary>
        internal void SaveSettings()
        {
            foreach(PropertyBase prop in _propList)
            {
                //условие, если файла с настройками еще нет
                if (_settings.ContainsKey(prop.propName))
                    _settings[prop.propName] = prop.propValue;
                else
                {
                    _settings.Add(prop.propName, prop.propValue);
                }
            }
            JsonFile.WriteSettingsInJson(_settings, _settingsFileName);
        }
    }

    #region Properties
    ///////////////////////////////////////
    ///описание методов в базовом классе///
    ///////////////////////////////////////

    /// <summary>Путь последней открытой директории.</summary>
    internal class LastPathProperty : PropertyBase
    {
        internal override void ChangeProperty(Dictionary<string, string> settings, string value)
        {
            propValue = value;
        }

        internal override void LoadProperty(Dictionary<string, string> settings)
        {
            propValue = settings[propName];
            if(propValue == "")
            {
                DriveInfo[] drive = DriveInfo.GetDrives();
                propValue = drive[0].Name;
            }
        }

        internal override void SetDefoltValues()
        {
            propName = "LastPath";

            DriveInfo[] drive = DriveInfo.GetDrives();
            propValue = drive[0].Name;
        }
    }

    /// <summary>Ширина окна.</summary>
    internal class WindowWidthProperty : PropertyBase
    {
        private string _defoltValue = "600";
        internal override void ChangeProperty(Dictionary<string, string> settings, string value)
        {
            propValue = value;
        }

        internal override void LoadProperty(Dictionary<string, string> settings)
        {
            propValue = settings[propName];
            if (propValue == "")
                propValue = _defoltValue;
        }

        internal override void SetDefoltValues()
        {
            propName = "WindowWidth";
            propValue = _defoltValue;
        }
    }

    /// <summary>Высота окна.</summary>
    internal class WindowHeightProperty : PropertyBase
    {
        private string _defoltValue = "400";
        internal override void ChangeProperty(Dictionary<string, string> settings, string value)
        {
            propValue = value;
        }

        internal override void LoadProperty(Dictionary<string, string> settings)
        {
            propValue = settings[propName];
            if (propValue == "")
                propValue = _defoltValue;
        }

        internal override void SetDefoltValues()
        {
            propName = "WindowHeight";
            propValue = _defoltValue;
        }
    }

    /// <summary>Количесво строк на одной странице.</summary>
    internal class StringCountProperty : PropertyBase
    {
        private string _defoltValue = "40";
        internal override void ChangeProperty(Dictionary<string, string> settings, string value)
        {
            propValue = value;
        }

        internal override void LoadProperty(Dictionary<string, string> settings)
        {
            propValue = settings[propName];
            if (propValue == "")
                propValue = _defoltValue;
        }

        internal override void SetDefoltValues()
        {
            propName = "StringCount";
            propValue = _defoltValue;
        }
    }

    #endregion
}
