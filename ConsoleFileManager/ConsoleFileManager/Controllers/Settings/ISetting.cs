
namespace ConsoleFileManager.Models
{
    interface ISetting
    {
        internal string PropValue { get; set; } //значение параметра.
        internal string PropName { get; }   //наименование параметра.
        internal void SaveProperty();   //метод сохранения параметра.
        internal void LoadProperty(); //метод загрузки параметра.
    }
}
