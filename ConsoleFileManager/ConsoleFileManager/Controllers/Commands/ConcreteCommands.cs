
using ConsoleFileManager.Controls;

namespace ConsoleFileManager.Controllers.Commands
{
    internal class DeleteCommand : ICommand
    {
        Controller reseiver;    //исполнитель команды

        public DeleteCommand(Controller r)
        {
            reseiver = r;
        }

        void ICommand.Execute()
        {
            reseiver.DeletingFile();
        }

        void ICommand.Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}
