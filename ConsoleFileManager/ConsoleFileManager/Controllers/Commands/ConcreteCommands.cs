﻿
using ConsoleFileManager.Controls;

namespace ConsoleFileManager.Controllers.Commands
{
    /// <summary>Команда удаления файла/папки.</summary>
    internal class DeleteCommand : Command
    {
        private Controller _reseiver;    //исполнитель команды

        public DeleteCommand(Controller r) => _reseiver = r;

        public override void Execute(string filename = "") => _reseiver.DeletingFile();

        public override void Undo(string filename = "") => throw new System.NotImplementedException();
    }

    /// <summary>Команда создания новой директории.</summary>
    internal class CreateDirectoryCommand : Command
    {
        private Controller _reseiver;

        public CreateDirectoryCommand(Controller r) => _reseiver = r;

        public override void Execute(string fileName) => _reseiver.CreateDirectory(fileName);

        public override void Undo(string filename = "") => throw new System.NotImplementedException();
    }

    /// <summary>Команда переименования файла/директории.</summary>
    internal class RenameCommand : Command
    {
        private Controller _reseiver;

        public RenameCommand(Controller r) => _reseiver = r;

        public override void Execute(string fileName) => _reseiver.Rename(fileName);

        public override void Undo(string filename = "") => throw new System.NotImplementedException();
    }

    /// <summary>Команда перемещения файла/папки в указанную директорию 
    /// (первое выполнение команды сохраняет путь перемещаемого файла
    /// второе - перемещает в выделенную директорию).</summary>
    internal class MoveCommand : Command
    {
        private Controller _reseiver;

        public MoveCommand(Controller r) => _reseiver = r;

        public override void Execute(string newPath = "") => _reseiver.Move();

        public override void Undo(string newPath = "") => throw new System.NotImplementedException();
    }

    /// <summary>Копирование файла/папки
    /// (первое выполнение команды сохраняет путь копируемого файла
    /// второе - копирует в выделенную директорию).</summary>
    internal class CopyCommand : Command
    {
        private Controller _reseiver;

        public CopyCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.Copy();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }

    /// <summary>Выделить файл выше по списку.</summary>
    internal class SelectTheTopOneCommand : Command
    {
        private Controller _reseiver;

        public SelectTheTopOneCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.SelectTheTopOne();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }

    /// <summary>Выделить файл ниже по списку.</summary>
    internal class SelectTheLowerOneCommand : Command
    {
        private Controller _reseiver;

        public SelectTheLowerOneCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.SelectTheLowerOne();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }

    /// <summary>Открыть выбранную директорию или запустить выбранный процесс.</summary>
    internal class ChangeDirectoryOrRunProcessCommand : Command
    {
        private Controller _reseiver;

        public ChangeDirectoryOrRunProcessCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.ChangeDirOrRunProcess();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }

    /// <summary>Выделить последний элемент на странице.</summary>
    internal class SelectLastOnPageCommand : Command
    {
        private Controller _reseiver;

        public SelectLastOnPageCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.SelectLastOnPage();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }

    /// <summary>Выделить первый элемент на странице.</summary>
    internal class SelectFirstOnPageCommand : Command
    {
        private Controller _reseiver;

        public SelectFirstOnPageCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.SelectFirstOnPage();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }

    /// <summary>Выделить последний элемент на странице.</summary>
    internal class PageUpOrDownCommand : Command
    {
        private Controller _reseiver;

        public PageUpOrDownCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.PageUp();

        public override void Undo(string b = "") => _reseiver.PageDown();
    }

    /// <summary>Завершение работы программы.</summary>
    internal class ExitCommand : Command
    {
        private Controller _reseiver;

        public ExitCommand(Controller r) => _reseiver = r;

        public override void Execute(string b = "") => _reseiver.ExitProgram();

        public override void Undo(string b = "") => throw new System.NotImplementedException();
    }
}
