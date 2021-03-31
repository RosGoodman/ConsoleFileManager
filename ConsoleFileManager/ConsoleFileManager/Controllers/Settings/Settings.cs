
using ConsoleFileManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace ConsoleFileManager.Controllers.Settings
{
    /// <summary>Класс описывающий действия с настройками.</summary>
    internal class Settings
    {
        private List<PropertyBase> _propList = new List<PropertyBase>();  //список настроек

        internal List<PropertyBase> PropList
        {
            get => _propList;
            set { _propList = value; }
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
                if (prop.PropName == propName)
                    prop.PropValue = value;
            }
        }

        /// <summary>Загрузть настрйки.</summary>
        internal void LoadSettings()
        {
            foreach(PropertyBase prop in _propList)
            {
                prop.LoadProperty();
            }
        }

        /// <summary>Сохранить настройки.</summary>
        internal void SaveSettings()
        {
            foreach(PropertyBase prop in _propList)
            {
                prop.SaveProperty();
            }
        }
    }

    #region Properties

    /// <summary>Путь последней открытой директории.</summary>
    internal class LastPathProperty : PropertyBase
    {
        internal override void LoadProperty()
        {
            PropValue = Settings1.Default.Path;
            if(PropValue == "")
            {
                DriveInfo[] drive = DriveInfo.GetDrives();
                PropValue = drive[0].Name;
            }
        }

        internal override void SaveProperty()
        {
            Settings1.Default.Path = PropValue;
            Settings1.Default.Upgrade();
            Settings1.Default.Save();
            
            Settings1.Default.Reload();
        }

        internal override void SetPropName()
        {
            PropName = "LastPath";
        }
    }

    /// <summary>Ширина окна.</summary>
    internal class WindowWidthProperty : PropertyBase
    {
        internal override void LoadProperty()
        {
            PropValue = Settings1.Default.WindowWidth.ToString();
        }

        internal override void SaveProperty()
        {
            if (int.TryParse(PropValue, out int n))
            {
                Settings1.Default.WindowWidth = n;
                Settings1.Default.Save();
                Settings1.Default.Reload();
                Settings1.Default.Upgrade();
                
            }
            else
                throw new Exception($"Попытка сохранить в {PropName} значение {PropValue}. Должно быть число.");
        }

        internal override void SetPropName()
        {
            PropName = "WindowWidth";
        }
    }

    /// <summary>Высота окна.</summary>
    internal class WindowHeightProperty : PropertyBase
    {
        internal override void LoadProperty()
        {
            PropValue = Settings1.Default.WindowHeight.ToString();
        }

        internal override void SaveProperty()
        {
            if (int.TryParse(PropValue, out int n))
            {
                Settings1.Default.WindowHeight = n;
                Settings1.Default.Save();
                Settings1.Default.Reload();
            }
            else
                throw new Exception($"Попытка сохранить в {PropName} значение {PropValue}. Должно быть число.");
        }

        internal override void SetPropName()
        {
            PropName = "WindowHeight";
        }
    }

    /// <summary>Количесво строк на одной странице.</summary>
    internal class StringCountProperty : PropertyBase
    {
        internal override void LoadProperty()
        {
            PropValue = Settings1.Default.StringCount.ToString();
        }

        internal override void SaveProperty()
        {
            if (int.TryParse(PropValue, out int n))
            {
                Settings1.Default.StringCount = n;
                Settings1.Default.Save();
                Settings1.Default.Reload();
            }
            else
                throw new Exception($"Попытка сохранить в {PropName} значение {PropValue}. Должно быть число.");
        }

        internal override void SetPropName()
        {
            PropName = "StringCount";
        }
    }

    #endregion
}
