
using ConsoleFileManager.Controls;

namespace ConsoleFileManager.Controllers.Commands
{
    /// <summary>Команда удаления файла/папки.</summary>
    internal class DeleteCommand : Command
    {
        private Controller _reseiver;    //исполнитель команды

        public DeleteCommand(Controller r) => _reseiver = r;

        public override void Execute(string filename) => _reseiver.DeletingFile(filename);

        public override void Undo() => throw new System.NotImplementedException();
    }

    /// <summary>Команда создания новой директории.</summary>
    internal class CreateDirectoryCommand : Command
    {
        private Controller _reseiver;

        public CreateDirectoryCommand(Controller r) => _reseiver = r;

        public override void Execute(string fileName) => _reseiver.CreateDirectory(fileName);

        public override void Undo() => throw new System.NotImplementedException();
    }

    /// <summary>Команда переименования файла/директории.</summary>
    internal class RenameCommand : Command
    {
        private Controller _reseiver;

        public RenameCommand(Controller r) => _reseiver = r;

        public override void Execute(string fileName) => _reseiver.Rename(fileName);

        public override void Undo() => throw new System.NotImplementedException();
    }

    /// <summary>Команда перемещения файла/папки в указанную директорию.</summary>
    internal class MoveCommand : Command
    {
        private Controller _resiver;

        public MoveCommand(Controller r) => _resiver = r;

        public override void Execute(string newPath) => _resiver.Move(newPath);

        public override void Undo() => throw new System.NotImplementedException();
    }
}
