
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
        private Controller _reseiver;

        public MoveCommand(Controller r) => _reseiver = r;

        public override void Execute(string newPath) => _reseiver.Move(newPath);

        public override void Undo() => throw new System.NotImplementedException();
    }

    /// <summary>Выделить файл выше по списку.</summary>
    internal class SelectTheTopOneCommand : Command
    {
        private Controller _reseiver;

        public SelectTheTopOneCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.SelectTheTopOne();

        public override void Undo() => throw new System.NotImplementedException();
    }

    /// <summary>Выделить файл ниже по списку.</summary>
    internal class SelectTheLowerOneCommand : Command
    {
        private Controller _reseiver;

        public SelectTheLowerOneCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.SelectTheLowerOne();

        public override void Undo() => throw new System.NotImplementedException();
    }
}
