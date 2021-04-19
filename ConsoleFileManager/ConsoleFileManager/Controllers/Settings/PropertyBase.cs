
using System.Collections.Generic;

namespace ConsoleFileManager.Models
{
    /// <summary>Класс описывающий параметры настроек.</summary>
    internal abstract class PropertyBase
    {
        internal string propValue { get; set; } //значение параметра.
        internal string propName { get; set; }   //наименование параметра.
        public PropertyBase()
        {
            SetDefoltValues();
        }
        internal abstract void SetDefoltValues();   //устанавливаем начальные настройки.
        internal abstract void ChangeProperty(Dictionary<string, string> settings, string value);    //изменение значения параметра.
        internal abstract void LoadProperty(Dictionary<string,string> settings);  //метод загрузки параметра.
    }
}
