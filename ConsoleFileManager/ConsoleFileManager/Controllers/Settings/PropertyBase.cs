
namespace ConsoleFileManager.Models
{
    /// <summary>Класс описывающий параметры настроек.</summary>
    internal abstract class PropertyBase
    {
        internal string PropValue { get; set; } //значение параметра.
        internal string PropName { get; set; }   //наименование параметра.
        public PropertyBase()
        {
            SetPropName();
        }
        internal abstract void SetPropName();   //устанавливаем имя свойства.
        internal abstract void SaveProperty();  //метод сохранения параметра.
        internal abstract void LoadProperty();  //метод загрузки параметра.
    }
}
