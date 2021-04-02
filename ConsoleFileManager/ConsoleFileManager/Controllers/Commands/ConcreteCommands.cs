
using ConsoleFileManager.Controls;

namespace ConsoleFileManager.Controllers.Commands
{
    internal class DeleteCommand : Command
    {
        private Controller _reseiver;    //исполнитель команды

        public DeleteCommand(Controller r)
        {
            _reseiver = r;
        }

        public override void Execute(string filename)
        {
            _reseiver.DeletingFile(filename);
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}
