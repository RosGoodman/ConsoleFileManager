
using ConsoleFileManager.Models;
using System.Collections.Generic;

namespace ConsoleFileManager.Controllers.Settings
{
    internal class Settings
    {
        internal List<ISetting> propList = new List<ISetting>();

        public Settings()
        {
            propList.Add(new LastPathProperty());
            LoadSettings();
        }

        internal void ChangeProperty(string propName, string value)
        {
            foreach(ISetting prop in propList)
            {
                if(prop.PropName)
            }
        }

        internal void LoadSettings()
        {
            foreach(ISetting prop in propList)
            {
                prop.LoadProperty();
            }
        }

        internal void SaveSettings()
        {
            foreach(ISetting prop in propList)
            {
                prop.SaveProperty();
            }
        }
    }

    internal class LastPathProperty : ISetting
    {
        //string ISetting.PropValue { get; set; }
        //string ISetting.PropName { get => "LastPath"; }

        //void ISetting.LoadProperty()
        //{
        //    PropValue = Settings1.Default.Path;
        //}

        //void ISetting.SaveProperty()
        //{
        //    Settings1.Default.Path = LastPathProp;
        //}
        string ISetting.PropValue { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        string ISetting.PropName => throw new System.NotImplementedException();

        void ISetting.LoadProperty()
        {
            throw new System.NotImplementedException();
        }

        void ISetting.SaveProperty()
        {
            throw new System.NotImplementedException();
        }
    }
}
