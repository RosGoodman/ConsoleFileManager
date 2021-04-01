
namespace ConsoleFileManager.Controllers.Commands
{
    internal interface ICommand
    {
        internal void Execute();
        internal void Undo();
    }
}
